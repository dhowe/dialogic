using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

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

            JsonNode.IDGEN = 0; // hack for new ids each request

            runtime.Chats().ForEach(chat =>
            {
                labels[chat.text] = new JsonNode(chat.text,
                    chat.OutgoingLabels(), chat.ToTree());
            });

            var jsCode = JsonNode.Escape(NodesToJS(labels));
            return Result.Success(jsCode).ToJSON(); 
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
                return Result.Error(e.Message).ToJSON();
            }

            if (result.IsNullOrEmpty()) result = "\"\"";

            return Result.Success(result).ToJSON();
        }

        public static string HandleRequest(HttpListenerRequest request)
        {
            IDictionary<string, string> kvs = ParsePostData(request);
            string type = kvs.ContainsKey("type") ? kvs["type"] : null;

            if (string.IsNullOrEmpty(type)) return Result.Error
                ("Empty request 'type': " + kvs.Stringify()).ToJSON();

            if (!ValidateKeys(type, kvs)) return Result.Error
                ("Badly-formed request" + kvs.Stringify()).ToJSON();

            Console.WriteLine("REQ: " + kvs.Stringify().Replace("\n", "\\n"));

            var result = "Invalid request type: " + type;

            if (type == "visualize") result = Visualize(kvs);
            if (type == "validate") result = Validate(kvs);
            if (type == "execute") result = Execute(kvs);

            Console.WriteLine("RES: " + result.Replace("\n","\\n") + "\n");

            return result;
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
            //Console.WriteLine("ParsePostData: " + request.HasEntityBody + " " + request.ContentType);

            var result = new Dictionary<string, string>();

            try
            {
                if (request.HasEntityBody)
                {
                    StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding);

                    if (request.ContentType.StartsWith("application/x-www-form-urlencoded", StringComparison.InvariantCulture)) // DCH: startsWith 11/10
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
                    else
                    {
                        Console.WriteLine("Unexpected Content-type: " + request.ContentType);
                    }

                    request.InputStream.Close();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                ChatRuntime.Warn(ex);
            }

            //Console.WriteLine("RESULT: " + request.Stringify());


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

        private static string NodesToJS(Dictionary<string, JsonNode> chatNodes)
        {
            var chats = "var chats = {\n";
            var nodes = "var nodes = new vis.DataSet([\n";
            var edges = "var edges = new vis.DataSet([\n";
            foreach (var node in chatNodes.Values)
            {
                chats += "  \"" + node.Id + "\": \"" + node.Data + "\",\n";
                nodes += "  { id: " + node.Id + ", label: '" + node.Name + "' },\n";
                node.Labels.ForEach(l => edges += "  { from: " + node.Id + ", to: "
                    + LookupNodeId(chatNodes, l) + " },\n");
            }
            chats += "};\n";
            nodes += "]);\n";
            edges += "]);";

            return chats + nodes + edges;
        }

        private static int LookupNodeId(Dictionary<string, JsonNode> dict, string key)
        {
            return dict.ContainsKey(key) ? dict[key].Id : -1; // TODO: editor should show warning here
        }
    }

    public class Result
    {
        public static string OK = "OK", ERR = "ERROR";

        public string Status;
        public string Data;
        public int LineNo = -1;

        public string ToJSON()
        {
            return "{ \"status\": \"" + Status + "\", \"lineNo\": "
                + LineNo + ", \"data\": \"" +  Data + "\" }";
        }

        public static Result Error(string error, Exception e = null, int lineNo = -1)
        {
            return new Result()
            {
                Data = e != null ? e.Message : error,
                LineNo = lineNo,
                Status = ERR
            };
        }

        public static Result Success(string code)
        {
            return new Result()
            {
                Status = OK,
                Data = code,
            };
        }
    }

    public class JsonNode
    {
        public static int IDGEN = 0;

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

        public override string ToString()
        {
            return "[" + Id + ": " + Name + " " + Labels.Stringify() + "]";
        }

        public static string Escape(string s)
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
                        sb.Append("\n");
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
