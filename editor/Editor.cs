using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Dialogic;
using Newtonsoft.Json;

namespace Dialogic.Server
{
    public class Editor
    {
        public static string SERVER_URL;
        public static int SERVER_PORT;

        static string PageContent;

        HttpListener listener;
        readonly Func<HttpListenerRequest, string> responderFunc;

        static ChatRuntime runtime;

        public Editor(Func<HttpListenerRequest, string> func, string host)
        {
            if (func == null) throw new ArgumentException
                ("Responder Func required");

            responderFunc = func;
            CreateNewListener(host).Start();
        }

        static List<int> usedPorts = new List<int>();
        static Random r = new Random();

        public HttpListener CreateNewListener(string hostname)
        {
            string uri = null;
            int newPort = -1;
            if (hostname == "rednoise.org")
            {
                uri = "http://rednoise.org:8082/dialogic/editor/";
                Console.WriteLine("Trying: " + uri + "...");
                listener = new HttpListener();
                listener.Prefixes.Add(uri);
                listener.Start();
            }
            else
            {
                while (true) // choose a random port
                {
                    listener = new HttpListener();
                    // IANA suggests the range 49152 to 65535 for private ports
                    newPort = r.Next(49152, 65535);
                    if (usedPorts.Contains(newPort))
                    {
                        continue;
                    }
                    uri = "http://" + hostname + ":" + newPort + "/dialogic/editor/";

                    listener.Prefixes.Add(uri);
                    try
                    {
                        listener.Start();
                    }
                    catch
                    {
                        continue;
                    }
                    usedPorts.Add(newPort);
                    break;
                }
            }
            Console.WriteLine("Running editor on " + uri);

            return listener;
        }

        public static string LocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    var local = ip.ToString();
                    if (local.StartsWithAny("127.", "192.168.", "10.", "172."))
                    {
                        local = "localhost";
                    }
                    else if (local == "138.16.162.17")
                    {
                        local = "rednoise.org";
                    }
                    return local;
                }
            }
            throw new Exception("No IPv4 network adapters with a valid address");
        }

        public void Run()
        {
            while (listener.IsListening)
            {
                ThreadPool.QueueUserWorkItem(c =>
                {
                    var ctx = c as HttpListenerContext;
                    try
                    {
                        if (ctx == null) return;

                        var rstr = responderFunc(ctx.Request);
                        var buf = Encoding.UTF8.GetBytes(rstr);
                        ctx.Response.ContentLength64 = buf.Length;
                        ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                    }
                    catch (Exception e)
                    {
                        ChatRuntime.Warn(e.ToString());
                    }
                    finally
                    {
                        try
                        {
                            if (ctx != null) ctx.Response.OutputStream.Close();
                        }
                        catch (Exception) { /* ignore */ }
                    }

                }, listener.GetContext());
            }
        }

        public void Stop()
        {
            try
            {
                listener.Stop();
                listener.Close();
            }
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
            catch (Exception) { /* ignore */ }
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
        }

        class JsonNode
        {

            private static int IDGEN = 0;

            public JsonNode(string name, List<string> labels, string data = null)
            {
                this.Id = ++IDGEN;
                this.Name = name;
                this.Labels = labels;
                this.Data = Escape(data);
            }

            public int Id { get; }
            public string Data { get; }
            public string Name { get; }
            public List<string> Labels { get; }

            private string Escape(string s)
            {
                if (string.IsNullOrEmpty(s)) return string.Empty;

                char c = '\0';
                StringBuilder sb = new StringBuilder(s.Length + 4);
                String t;

                for (var i = 0; i < s.Length; i += 1)
                {
                    c = s[i];
                    switch (c)
                    {
                        case '\\':
                        case '"':
                            sb.Append('\\');
                            sb.Append(c);
                            break;
                        case '/':
                            sb.Append('\\');
                            sb.Append(c);
                            break;
                        case '\b':
                            sb.Append("\\b");
                            break;
                        case '\t':
                            sb.Append("\\t");
                            break;
                        case '\n':
                            sb.Append("\\n");
                            break;
                        case '\f':
                            sb.Append("\\f");
                            break;
                        case '\r':
                            sb.Append("\\r");
                            break;
                        default:
                            if (c < ' ')
                            {
                                t = "000" + String.Format("X", c);
                                sb.Append("\\u" + t.Substring(t.Length - 4));
                            }
                            else
                            {
                                sb.Append(c);
                            }
                            break;
                    }
                }

                return sb.ToString();
            }
        }

        public static string SendVisualizerResponse(HttpListenerRequest request)
        {
            var testfile = "data/network.gs";
            var html = PageContent.Replace("%%URL%%", SERVER_URL);
            var labels = new Dictionary<string, JsonNode>();

            ISerializer serial = new Client.SerializerMessagePack();

            runtime = new ChatRuntime(Client.AppConfig.TAC);
            runtime.ParseFile(new FileInfo(testfile));
            runtime.Chats().ForEach(chat =>
            {
                labels[chat.text] = new JsonNode(chat.text, chat.OutgoingLabels(), chat.ToTree());
                //JsonConvert.SerializeObject(ChatData.Create(chat)));
            });

            var json = NodesToJSON(labels);

            html = html.Replace("%%DATA%%", json);

            Console.WriteLine(json);

            return html;
        }

        private static string NodesToJSON(Dictionary<string, JsonNode> chatNodes)
        {
            var chats = "\nvar chats = {\n";
            var nodes = "\nvar nodes = new vis.DataSet([\n";
            var edges = "\nvar edges = new vis.DataSet([\n";
            foreach (var node in chatNodes.Values)
            {
                chats += "  \"" + node.Id + "\": \"" + node.Data + "\",\n";
                nodes += "  { id: " + node.Id + ", label: '" + node.Name + "' },\n";
                node.Labels.ForEach(l => edges += "  { from: " + node.Id + ", to: "
                    + chatNodes[l].Id + " },\n");
            }
            chats += "};\n";
            nodes += "]);\n";
            edges += "]);\n";

            return chats + nodes + edges;
        }

        public static string SendEditorResponse(HttpListenerRequest request)
        {
            var html = PageContent.Replace("%%URL%%", SERVER_URL);
            var wmsg = "Enter your script here";

            IDictionary<string, string> kvs = ParsePostData(request);
            var path = kvs.ContainsKey("path") ? kvs["path"] : null;
            var code = kvs.ContainsKey("code") ? kvs["code"] : null;
            var mode = kvs.ContainsKey("mode") ? kvs["mode"] : "validate";

            // fetch code from file
            if (!string.IsNullOrEmpty(path)) code = new WebClient().DownloadString(path);

            // default info message
            if (string.IsNullOrEmpty(code)) return html.Replace("%%CODE%%", wmsg);

            html = html.Replace("%%CODE%%", WebUtility.HtmlEncode(code));
            html = html.Replace("%%CCLASS%%", "shown");

            // only process the selection if there is one
            if (kvs.ContainsKey("selectionStart"))
            {
                code = kvs["selection"];
                html = html.Replace("%%STARTINDEX%%", kvs["selectionStart"]);
                html = html.Replace("%%ENDINDEX%%", kvs["selectionEnd"]);
            }

            try
            {
                string content = string.Empty;
                var globals = new Dictionary<string, object>();
                runtime = new ChatRuntime(Client.AppConfig.TAC);
                runtime.strictMode = false; // allow unbound symbols/functions
                runtime.ParseText(code, !kvs.ContainsKey("useValidators")
                    || !kvs["useValidators"].Equals("true")); // true to disable validators
                runtime.Chats().ForEach(c => { content += c.ToTree() + "\n\n"; });

                var result = string.Empty;
                if (mode == "execute")
                {
                    // first run any chats marked with 'preload=true'
                    runtime.Preload(globals);

                    // run the first chat with all timing disabled
                    result = WebUtility.HtmlEncode(runtime.InvokeImmediate(globals));

                    if (result.IsNullOrEmpty()) result = "[empty-string]";
                }

                html = html.Replace("%%RESULT%%", WebUtility.HtmlEncode(content));
                html = html.Replace("%%EXECUTE%%", result);
                html = html.Replace("%%RCLASS%%", "success");
            }
            catch (ParseException ex)
            {
                OnError(ref html, ex, ex.lineNumber);
            }
            catch (Exception e)
            {
                OnError(ref html, e, -1);
            }

            return html;
        }

        private static void OnError(ref string html, Exception ex, int lineno = -1)
        {
            Console.WriteLine("[CAUGHT] " + ex);

            html = html.Replace("%%EXECUTE%%", "");
            html = html.Replace("%%RCLASS%%", "error");
            html = html.Replace("%%RESULT%%", lineno < 0 ? ex.ToString() : ex.Message);
            html = html.Replace("%%ERRORLINE%%", lineno < 0 ? "" : lineno.ToString());

            if (lineno < 0) Console.WriteLine("[STACK]\n" + ex.Message);
        }

        private static IDictionary<string, string> ParsePostData(HttpListenerRequest request)
        {
            var result = new Dictionary<string, string>();

            try
            {
                if (request.HasEntityBody)
                {
                    Stream body = request.InputStream;
                    Encoding encoding = request.ContentEncoding;
                    StreamReader reader = new System.IO.StreamReader(body, encoding);

                    if (request.ContentType == "application/x-www-form-urlencoded")
                    {
                        string s = reader.ReadToEnd();
                        string[] pairs = s.Split('&');

                        foreach (var p in pairs)
                        {
                            var pair = p.Split('=');
                            if (pair.Length == 2)
                            {
                                //Console.WriteLine(pair[0] + ": " + pair[1]);
                                result.Add(WebUtility.UrlDecode(pair[0]),
                                    WebUtility.UrlDecode(pair[1]));
                            }
                            else
                            {
                                ChatRuntime.Warn("BAD KV-PAIR: " + p);
                                //throw new Exception("BAD-PAIR: " + p);
                            }
                        }
                    }

                    body.Close();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                ChatRuntime.Warn(ex);
            }

            return result;
        }


        public static void Main(string[] args)
        {
            var runViz = true;
            var pageContent = "data/index.html";
            var host = args.Length > 0 ? args[0] : "localhost";
            Func<HttpListenerRequest, string> listener = SendEditorResponse;

            if (runViz)
            {
                pageContent = "data/combined.html";
                listener = SendVisualizerResponse;
            }

            Editor ws = new Editor(listener, host);
            Editor.PageContent = string.Join
                ("\n", File.ReadAllLines(pageContent, Encoding.UTF8));

            ws.Run();
        }
    }
}

