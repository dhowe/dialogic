using System;
using System.Net;
using System.Text;
using System.Threading;

#pragma warning disable RECS0022

namespace Dialogic.Server
{
    /*
     * To run, enter the editor directory and do $ dotnet build && dotnet run
     * which will start the server at: http://localhost:8082/dialogic-workbench/
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
        const string SERVER_PATH = "/dialogic-server/";

        HttpListener listener;
        readonly Func<HttpListenerRequest, string> responder;

        public DialogicServer(Func<HttpListenerRequest, string> func, string host)
        {
            if (func == null) throw new ArgumentException("Responder required");

            this.responder = func;
            CreateNewListener(host).Start();
        }

        public HttpListener CreateNewListener(string hostname)
        {
            string uri = "http://" + hostname + ":8082" + SERVER_PATH;

            listener = new HttpListener();
            listener.Prefixes.Add(uri);
            listener.Start();

            Console.WriteLine("Running dialogic-server on " + uri);

            return listener;
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

                        var buf = Encoding.UTF8.GetBytes(responder(ctx.Request));
                        ctx.Response.ContentLength64 = buf.Length;
                        ctx.Response.ContentType = "application/json";
                        ctx.Response.AppendHeader("Access-Control-Allow-Origin", "*");
                        ctx.Response.AppendHeader("Access-Control-Allow-Methods", "POST.OPTIONS");
                        ctx.Response.AppendHeader("Access-Control-Allow-Headers", "Content-Type");
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

        public static void Main(string[] args)
        {
            var host = args.Length > 0 ? args[0] : "localhost";
            new DialogicServer(RequestHandler.HandleRequest, host).Run();
        }
    }
}
