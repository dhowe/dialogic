using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Dialogic;
using Newtonsoft.Json;

#pragma warning disable RECS0022

namespace Dialogic.NewServer
{
    /**
     * To run, enter the editor directory and do $ dotnet build && dotnet run
     * which will start the server at: http://localhost:8082/dialogic/editor/
     *
     * There are 3 server functions, validate, execute, and visualize, each of which can be called via an HTTP Post
     * request. Each returns a JSON object with 3 fields: 'status', 'data', and 'lineNo'.
     *
     * If the request was successful, 'status' will be 'OK', and the requested data will be in the 'data' field.
     * If the 'status' is 'ERROR', then 'data' will contain the message to display, and 'lineNo' will have the line number.
     *
     * Each request include key-value pairs (as Post data). The only required key is 'type', whose value must be one of 'validate', 'execute', or 'visualize'.
     * Additional valid parameters for each type are included below.
     *
     * 'validate': type = validate, code = codeToValidate, useValidators = true|false
     *
     * 'execute': type = execute, code = codeToExecute, useValidators = true|false
     *
     * 'visualize': type = visualize, code = codeToVisualize (all chats)
     */
    public class DialogicServer
    {
        const string SERVER_PATH = "/dialogic/editor/";

        public static string SERVER_URL;
        public static int SERVER_PORT;

        HttpListener listener;
        readonly Func<HttpListenerRequest, string> responder;

        public DialogicServer(Func<HttpListenerRequest, string> func, string host)
        {
            if (func == null) throw new ArgumentException("Responder required");

            this.responder = func;
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
                uri = "http://rednoise.org:8082" + SERVER_PATH;
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

                    // IANA suggests 49152-65535 for private ports
                    newPort = r.Next(49152, 65535);

                    if (usedPorts.Contains(newPort)) continue;

                    newPort = 8082;

                    uri = "http://" + hostname + ":" + newPort + SERVER_PATH;

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
            Console.WriteLine("Running dialogic-server on " + uri);

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
                        ctx.Response.AppendHeader("Access-Control-Allow-Origin", "*");
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

        /*public static string RequestHandlerX(HttpListenerRequest request)
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
        }*/

        public static void Main(string[] args)
        {
            var host = args.Length > 0 ? args[0] : "localhost";
            new DialogicServer(RequestHandler.HandleRequest, host).Run();
        }
    }
}
