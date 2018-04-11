using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;

using Dialogic;

namespace Dialogic.Server
{
    public class LintServer
    {
        public static string SERVER_URL = "http://" + LocalIPAddress() + ":8080/glint/";

        static Regex Brackets = new Regex(@"(\]|\[)");
        static string IndexPageContent;

        static bool noValidators = false;

        readonly HttpListener listener = new HttpListener();
        readonly Func<HttpListenerRequest, string> responderFunc;

        static ChatRuntime runtime;

        public LintServer(Func<HttpListenerRequest, string> func, params string[] prefixes)
        {
            if (prefixes == null || prefixes.Length == 0) throw new ArgumentException("URI required");

            if (func == null) throw new ArgumentException("responder required");

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
                    return ip.ToString();
                }
            }
            throw new Exception("No IPv4 network adapters with a valid address");
        }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                try
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
                            catch { /* ignored */ }
                            finally
                            {
                                // always close the stream
                                if (ctx != null) ctx.Response.OutputStream.Close();
                            }
                        }, listener.GetContext());
                    }
                }
                catch (Exception) { /* ignored */ }
            });
        }

        public void Stop()
        {
            listener.Stop();
            listener.Close();
        }

        public static string SendResponse(HttpListenerRequest request)
        {
            var html = IndexPageContent.Replace("%%URL%%", SERVER_URL);

            var kvs = ParsePostData(request);

            var code = kvs.ContainsKey("code") ? kvs["code"] : null;
            var mode = kvs.ContainsKey("mode") ? kvs["mode"] : "validate";

            if (String.IsNullOrEmpty(code))
            {
                return html.Replace("%%CODE%%", "Enter your code here");
            }

            html = html.Replace("%%CODE%%", code);
            html = html.Replace("%%CCLASS%%", "shown");

            try
            {
                string content = ParserText(code, noValidators);
                runtime.chats.ForEach(c=>c.Realize(null));
                if (mode == "execute")
                {
                    var cmd = runtime.chats.Last().commands.Last();
                    content += cmd.Text(false) + " -> " + cmd.Text(true);
                }

                html = html.Replace("%%RESULT%%", content);
                html = html.Replace("%%RCLASS%%", "success");
            }
            catch (ParseException ex)
            {
                html = html.Replace("%%RCLASS%%", "error");
                html = html.Replace("%%RESULT%%", ex.Message);

                var lineNo = ex.lineNumber;
                html = html.Replace("%%ERRORLINE%%", lineNo + "");
            }

            return html;
        }

        private static string ParserText(string code, bool noVal = false)
        {
            string content = String.Empty;
            runtime = new ChatRuntime(Tendar.AppConfig.Actors);
            runtime.ParseText(code, noVal);
            runtime.chats.ForEach(c => { content += c.ToTree() + "\n\n"; });
            return content;
        }

        private static IDictionary<string, string> ParsePostData(HttpListenerRequest request)
        //private static string ParsePostData(HttpListenerRequest request)
        {

            IDictionary<string, string> result = new Dictionary<string, string>();

            if (request.HasEntityBody)
            {
                Stream body = request.InputStream;
                Encoding encoding = request.ContentEncoding;
                StreamReader reader = new System.IO.StreamReader(body, encoding);

                if (request.ContentType == "application/x-www-form-urlencoded")
                {
                    //string s = Uri.UnescapeDataString(reader.ReadToEnd());
                    string s = WebUtility.UrlDecode(reader.ReadToEnd());
                    string[] pairs = s.Split('&');
                    foreach (var p in pairs)
                    {
                        var pair = p.Split('=');
                        if (pair.Length == 2)
                        {
                            result.Add(pair[0], pair[1]);
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
            ws.Run();

            Console.WriteLine("LintServer running on "
                + SERVER_URL + " - press any key to quit");
            Console.ReadKey();
            ws.Stop();
        }
    }
}
