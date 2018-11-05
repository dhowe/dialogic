using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Dialogic;
using Newtonsoft.Json;

namespace Dialogic.NewServer
{
    public class DialogicServer
    {
        public static string SERVER_URL;
        public static int SERVER_PORT;

        HttpListener listener;
        readonly Func<HttpListenerRequest, string> responder;

        static ChatRuntime runtime;

        public DialogicServer(Func<HttpListenerRequest, string> func, string host)
        {
            if (func == null) throw new ArgumentException
                ("Responder Func required");

            responder = func;
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

                        var rstr = responder(ctx.Request);
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

     


        public static string RequestHandlerX(HttpListenerRequest request)
        {

            IDictionary<string, string> kvs = ParsePostData(request);
            string type = kvs.ContainsKey("type") ? kvs["type"] : null;
            if (string.IsNullOrEmpty(type)) return "Empty request type";

            if (type == "visualize") return Visualize(kvs);
            if (type == "validate") return Validate(kvs);
            if (type == "execute") return Execute(kvs);

            return "Invalid request type: " + type;


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

        public static void Main(string[] args)
        {
            var host = args.Length > 0 ? args[0] : "localhost";
            new DialogicServer(RequestHandler.HandleRequest, host).Run();
        }
    }
}

