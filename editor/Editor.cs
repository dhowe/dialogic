using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Dialogic;

namespace Dialogic.Server
{
    public class Editor
    {
        public static string SERVER_URL;
        public static int SERVER_PORT;

        static string IndexPageContent;

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
            catch (Exception) { /* ignore */ }
        }

        public static string SendResponse(HttpListenerRequest request)
        {
            var html = IndexPageContent.Replace("%%URL%%", SERVER_URL);

            IDictionary<string, string> kvs = new Dictionary<string, string>();
            try
            {
                kvs = ParsePostData(request);
            }
            catch (Exception ex)
            {
                ChatRuntime.Warn(ex);
            }

            var path = kvs.ContainsKey("path") ? kvs["path"] : null;
            var code = kvs.ContainsKey("code") ? kvs["code"] : null;
            var mode = kvs.ContainsKey("mode") ? kvs["mode"] : "validate";

            if (!String.IsNullOrEmpty(path)) // fetch code from file
            {
                using (var wb = new WebClient()) code = wb.DownloadString(path);
            }

            if (String.IsNullOrEmpty(code))
            {
                return html.Replace("%%CODE%%", "Enter your script here");
            }

            html = html.Replace("%%CODE%%", WebUtility.HtmlEncode(code));
            html = html.Replace("%%CCLASS%%", "shown");

            // only process the selection if there is one
            if (kvs.ContainsKey("selectionStart")) {
                 code = kvs["selection"];
                 html = html.Replace("%%STARTINDEX%%", kvs["selectionStart"]);
                 html = html.Replace("%%ENDINDEX%%", kvs["selectionEnd"]);
            }
           
            try
            {
                string content = String.Empty;
                var globals = new Dictionary<string, object>();
                runtime = new ChatRuntime(Client.AppConfig.TAC);
                runtime.strictMode = false; // allow unbound symbols/functions
                runtime.ParseText(code, kvs.ContainsKey("useValidators") && kvs["useValidators"].Equals("true") ? false : true); // true to disable validators
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
                            ChatRuntime.Warn("BAD KV - PAIR: " + p);
                            //throw new Exception("BAD-PAIR: " + p);
                        }
                    }
                }

                body.Close();
                reader.Close();
            }

            return result;
        }

        //private static string MakeServerUrl(string host)
        //{
        //    return "http://" + host + ":8082/dialogic/editor/";
        //}

        public static void Main(string[] args)
        {
            var host = args.Length > 0 ? args[0] : "localhost";
            Editor ws = new Editor(SendResponse, host);

            Editor.IndexPageContent = String.Join
                ("\n", File.ReadAllLines("data/index.html", Encoding.UTF8));

            ws.Run();
        }
    }
}

