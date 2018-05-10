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
    public class LintServer
    {
        public static string SERVER_URL = "http://" +
            LocalIPAddress() + ":8081/dialogic/editor/";

        static string IndexPageContent;

        readonly HttpListener listener = new HttpListener();
        readonly Func<HttpListenerRequest, string> responderFunc;

        static ChatRuntime runtime;

        public LintServer(Func<HttpListenerRequest, string> func,
            params string[] prefixes)
        {
            if (prefixes == null || prefixes.Length == 0)
            {
                throw new ArgumentException("URI required");
            }

            if (func == null) throw new ArgumentException
                ("Responder Func required");

            foreach (var s in prefixes) listener.Prefixes.Add(s);

            responderFunc = func;
            listener.Start();
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
            code = kvs.ContainsKey("selection") ? kvs["selection"] : code;

            try
            {
                string content = String.Empty;
                var globals = new Dictionary<string, object>();
                runtime = new ChatRuntime(Tendar.AppConfig.Actors);
                runtime.strictMode = false; // allow unbound symbols/functions
                runtime.ParseText(code, false); // true to disable validators
                runtime.Chats().ForEach(c => { content += c.ToTree() + "\n\n"; });

                var result = string.Empty;
                if (mode == "execute")
                {
                    runtime.Preload(globals);       // run any preload=true chats
                    Console.WriteLine("GLOBALS="+globals.Stringify());
                    // run the first chats with all timing disabled
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

        public static void Main()
        {
            string html = String.Join("\n",
            File.ReadAllLines("data/index.html", Encoding.UTF8));

            LintServer ws = new LintServer(SendResponse, SERVER_URL);
            LintServer.IndexPageContent = html;

            Console.WriteLine("LintServer running on " + SERVER_URL);

            ws.Run();
        }
    }
}

