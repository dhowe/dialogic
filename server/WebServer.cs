using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace Dialogic.Server
{
    public class WebServer
    {
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerRequest, string> _responderMethod;
        private static string IndexPageContent;

        public WebServer(IReadOnlyCollection<string> prefixes, Func<HttpListenerRequest, string> method)
        {
            if (prefixes == null || prefixes.Count == 0) throw new ArgumentException("URI required");

            if (method == null) throw new ArgumentException("responder required");

            foreach (var s in prefixes) _listener.Prefixes.Add(s);

            _responderMethod = method;
            _listener.Start();
        }

        public WebServer(Func<HttpListenerRequest, string> method, params string[] prefixes)
           : this(prefixes, method) {}

        public void Run()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                Console.WriteLine("Webserver running...");
                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem(c =>
                        {
                            var ctx = c as HttpListenerContext;
                            try
                            {
                                if (ctx == null) return;

                                var rstr = _responderMethod(ctx.Request);
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
                        }, _listener.GetContext());
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            });
        }

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }

        public static string SendResponse(HttpListenerRequest request)
        {
            var code = request.QueryString.Get("code");

            if (String.IsNullOrEmpty(code)) return IndexPageContent;

            Console.WriteLine("code: '" + code + "'");

            string result = "<HTML><BODY>" + code + "<BR>" + DateTime.Now + "</BODY></HTML>";

            // NEXT: Append parse-results to html IndexPageContent here

            return string.Format(result, DateTime.Now);
        }

        public static void Main(string[] args)
        {
            WebServer ws = new WebServer(SendResponse, "http://localhost:8083/lint/");
            WebServer.IndexPageContent = String.Join("\n", File.ReadAllLines("data/index.html", Encoding.UTF8));
            ws.Run();
            Console.WriteLine("A simple webserver. Press a key to quit.");
            Console.ReadKey();
            ws.Stop();
        }
    }
}
