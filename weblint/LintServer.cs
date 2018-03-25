using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
                            catch
                            {
                                // ignored
                            }
                            finally
                            {
                                // always close the stream
                                if (ctx != null)
                                {
                                    ctx.Response.OutputStream.Close();
                                }
                            }
                        }, listener.GetContext());
                    }
                }
                catch (Exception) { }
            });
        }

        public void Stop()
        {
            listener.Stop();
            listener.Close();
        }

        public static string SendResponse(HttpListenerRequest request)
        {
            var html = IndexPageContent;

            var code = request.QueryString.Get("code");
            if (String.IsNullOrEmpty(code))
            {
                return html.Replace("%%CODE%%", "Enter your code here");
            }

            html = html.Replace("%%CODE%%", code);
            html = html.Replace("%%CCLASS%%", "shown");

            try
            {
                string content = ParserText(code, noValidators);

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

        private static string ParserText(string code, bool noVal=false)
        {
            string content = String.Empty;
            new ChatRuntime(Tendar.AppConfig.Actors).ParseText(code, noVal)
                .ForEach(c => { content += c.ToTree() + "\n\n"; });
            return content;
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
