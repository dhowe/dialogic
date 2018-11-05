using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Dialogic.NewServer
{
    public static class RequestHandler
    {
        static ChatRuntime runtime;
        static IDictionary<string, object> globals;

        public static string Visualize(IDictionary<string, string> kvs)
        {
            var code = kvs.ContainsKey("code") ? kvs["code"] : null;
            if (code.IsNullOrEmpty()) return Result.Error
                ("Missing 'code' arg: " + kvs.Stringify()).ToJSON();

            var parsed = ParseScript(kvs);
            if (parsed.Status != Result.OK) return parsed.ToJSON();

            var labels = new Dictionary<string, JsonNode>();

            runtime.Chats().ForEach(chat =>
            {
                labels[chat.text] = new JsonNode(chat.text,
                    chat.OutgoingLabels(), chat.ToTree());
            });

            return NodesToJSON(labels);
        }

        public static string Validate(IDictionary<string, string> kvs)
        {
            return ParseScript(kvs).ToJSON();
        }

        public static string Execute(IDictionary<string, string> kvs)
        {
            var parsed = ParseScript(kvs);
            if (parsed.Status != Result.OK) return parsed.ToJSON();

            var result = string.Empty;

            try
            {
                if (runtime == null) throw new Exception("null runtime");

                runtime.Preload(globals);
                result = runtime.InvokeImmediate(globals);
                result = WebUtility.HtmlEncode(result);
            }
            catch (Exception e)
            {
                return Result.Error(e.Message, e, -1).ToJSON();
            }

            if (result.IsNullOrEmpty()) result = "\"\"";

            return Result.Success(result).ToJSON();
        }

        public static string HandleRequest(HttpListenerRequest request)
        {
            IDictionary<string, string> kvs = ParsePostData(request);
            string type = kvs.ContainsKey("type") ? kvs["type"] : null;

            if (string.IsNullOrEmpty(type))
            {
                return Result.Error("Empty request 'type': " + kvs.Stringify()).ToJSON();
            }

            if (!ValidateKeys(type, kvs))
            {
                return Result.Error("Badly-formed request" + kvs.Stringify()).ToJSON();
            }


            if (type == "visualize") return Visualize(kvs);
            if (type == "validate") return Validate(kvs);
            if (type == "execute") return Execute(kvs);

            return "Invalid request type: " + type;
        }

        // -------------–-------------–-------------–-------------–-------------–-----------

        private static Result ParseScript(IDictionary<string, string> kvs)
        {
            var code = kvs.ContainsKey("code") ? kvs["code"] : null;

            var validators = kvs.ContainsKey("useValidators")
                && kvs["useValidators"].Equals("true");

            if (kvs.ContainsKey("selectionStart"))
            {
                code = kvs["selection"];
                var startIdx = kvs["selectionStart"];
                var endIdx = kvs["selectionEnd"];
            }

            try
            {
                globals = new Dictionary<string, object>();
                runtime = new ChatRuntime(Client.AppConfig.TAC);
                runtime.strictMode = false;      // allow unbound symbols/functions
                runtime.ParseText(code, !validators); // true to disable validators
            }
            catch (ParseException ex)
            {
                return Result.Error(code, ex, ex.lineNumber);
            }
            catch (Exception e)
            {
                return Result.Error(code, e, -1);
            }

            //Console.WriteLine("PARSE: "+ code);

            return Result.Success(code);
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


        static bool ValidateKeys(string type, IDictionary<string, string> kvs)
        {
            if (type == "visualize")
            {
                return true;
            }
            if (type == "validate")
            {
                return true;
            }
            if (type == "execute")
            {
                return true;
            }
            return false;
        }

        private static string OnError(string msg, Exception ex, int lineno = -1)
        {
            return "ERROR: " + msg;
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

    }

    internal class Result
    {
        internal static string OK = "OK";
        internal static string ERR = "ERROR";

        Result()
        {
        }

        internal string Status;
        internal string Data;
        internal int LineNo = -1;
        //Exception Exception;

        internal string ToJSON()
        {

            //return JsonConvert.SerializeObject(this);
            return "{ \"status\": \"" + Status + "\", \"lineNo\":"
                + LineNo + ", \"data\": \"" + Data + "\" }";
        }

        internal static Result Error(string error, Exception e = null, int lineNo = -1)
        {
            return new Result()
            {
                Status = ERR,
                Data = e.Message,
                LineNo = lineNo
            };
        }

        internal static Result Success(string code)
        {
            return new Result()
            {
                Status = OK,
                Data = code,
            };
        }
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
}
