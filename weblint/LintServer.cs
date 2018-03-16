using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Dialogic;

namespace Dialogic.Server
{
    public class LintServer
    {
        const string SERVER_URL = "http://localhost:8083/lint/";

        private static string indexPageContent;

        private readonly HttpListener listener = new HttpListener();
        private readonly Func<HttpListenerRequest, string> responderMethod;

        public LintServer(Func<HttpListenerRequest, string> method, params string[] prefixes)
        {
            if (prefixes == null || prefixes.Length == 0) throw new ArgumentException("URI required");

            if (method == null) throw new ArgumentException("responder required");

            foreach (var s in prefixes) listener.Prefixes.Add(s);

            responderMethod = method;
            listener.Start();
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

                                var rstr = responderMethod(ctx.Request);
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
                catch (Exception)
                {
                    // ignored
                }
            });
        }

        public void Stop()
        {
            listener.Stop();
            listener.Close();
        }

        public static string SendResponse(HttpListenerRequest request)
        {
            var html = indexPageContent;
            var code = request.QueryString.Get("code");

            if (String.IsNullOrEmpty(code))
            {
                return html.Replace("%%CODE%%", "Input your code here");
            }

            code = code.Replace("%%BR%%", "\n");

            //Console.WriteLine("code: '" + code + "'");

            html = html.Replace("%%CODE%%", code);
            html = html.Replace("%%CCLASS%%", "shown");

            List<Chat> chats = null;
            try
            {
                string content = String.Empty;
                chats = ChatParser.ParseText(code);
                chats.ForEach(c => content += c.ToTree());
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

        public static void Main()
        {
            string html = String.Join("\n",
                File.ReadAllLines("data/index.html", Encoding.UTF8));

            LintServer ws = new LintServer(SendResponse, SERVER_URL);
            LintServer.indexPageContent = html;
            ws.Run();

            Console.WriteLine("LintServer running... press any key to quit");
            Console.ReadKey();
            ws.Stop();
        }
    }
}
