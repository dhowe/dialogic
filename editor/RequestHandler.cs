using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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

            var nodes = new List<JsonNode>();
            var chats = runtime.Chats();

            chats.ForEach(chat =>
            {
                nodes.Add(new JsonNode(chat.text, chat.OutgoingLabels(), chat.ToTree()));
            });

            var json = JsonConvert.SerializeObject(nodes);
            json = json.Replace("\"", "\\\""); // yuck
            //Console.WriteLine("JSN: " + json);

            var result = Result.Success(json).ToJSON();
            //Console.WriteLine("Res: " + result);
            return result;
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
                if (runtime == null) throw new Exception("no runtime");

                runtime.Preload(globals);
                result = runtime.InvokeImmediate(globals);
                result = WebUtility.HtmlEncode(result);
            }
            catch (Exception e)
            {
                return Result.Error(e.Message).ToJSON();
            }

            if (result.IsNullOrEmpty()) result = " ";

            return Result.Success(result).ToJSON();
        }

        public static string HandleRequest(HttpListenerRequest request)
        {

            IDictionary<string, string> kvs = ParsePostData(request);
            string type = kvs.ContainsKey("type") ? kvs["type"] : null;

            if (string.IsNullOrEmpty(type)) return Result.Error
                ("Empty request 'type': " + kvs.Stringify()).ToJSON();

            if (!ValidateKeys(type, kvs)) return Result.Error
                ("Badly-formed request: " + kvs.Stringify()).ToJSON();

            // NOTE: logging incorrect in OS X terminal, use built-in console if needed

            var regex = new Regex(@"\r?\n");

            Console.WriteLine("REQ: " + regex.Replace(kvs.Stringify(), "\\n"));

            var result = "Invalid request type: " + type;

            if (type == "visualize") result = Visualize(kvs);
            if (type == "validate") result = Validate(kvs);
            if (type == "execute") result = Execute(kvs);

            //Console.WriteLine("PRE: " + result + "\n");

            result = regex.Replace(result, "\\\n");

            Console.WriteLine("RES: " + result + "\n");

            return result;
        }

        // -------------–-------------–-------------–-------------–-------------–-----------

        private static Result ParseScript(IDictionary<string, string> kvs)
        {
            var code = kvs.ContainsKey("code") ? kvs["code"] : null;

            var validators = kvs.ContainsKey("useValidators")
                && kvs["useValidators"].Equals("true");

            //if (kvs.ContainsKey("selectionStart")) // TODO: handle in JS
            //{
            //    code = kvs["selection"];
            //    var startIdx = kvs["selectionStart"];
            //    var endIdx = kvs["selectionEnd"];
            //}

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

            return Result.Success(code);
        }

        private static IDictionary<string, string> ParsePostData(HttpListenerRequest request)
        {
            Dictionary<string, string> result = null; //new Dictionary<string, string>();

            try
            {
                if (request.HasEntityBody)
                {
                    StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding);

                    if (request.ContentType.StartsWith("application/json",
                        StringComparison.InvariantCulture)) //== -> startsWith 11/10
                    {
                        string json = reader.ReadToEnd();
                        //Console.WriteLine("json: " + json);
                        result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    }
                    else
                    {
                        Console.WriteLine("Unexpected Content-type: " + request.ContentType);
                    }

                    request.InputStream.Close();
                    reader.Close();
                }
                else
                {
                    result = new Dictionary<string, string>(); // no-op
                }
            }
            catch (Exception ex)
            {
                ChatRuntime.Warn(ex);
            }

            return result;
        }

        private static bool ValidateKeys(string type, IDictionary<string, string> kvs)
        {
            // must have non-empty code key
            return !string.IsNullOrEmpty(kvs["code"]);
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
                + LineNo + ", \"data\": \"" + Data + "\" }";
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
        //public static int IDGEN = 0;

        public JsonNode(string name, List<string> labels, string data = null)
        {
            //this.Id = ++IDGEN;
            this.Name = name;
            this.Labels = labels; //new List<int>();
            //labels.ForEach(l => this.Labels.Add())
            this.Data = data; //Escape(data);
        }

        //public int Id { get; }
        public string Data { get; }
        public string Name { get; }
        public List<string> Labels { get; }

  
        public override string ToString()
        {
            //return "[" + Id + ": " + Name + " " + Labels.Stringify() + "]";
            return "[" + Name + ": " + Labels.Stringify() + "]";
        }

        /*public override string ToJSON()
        {
            var json = "{ \"id\": \"" + Id + "\", \"name\":\"" + Name + "\", \"data\": " + Data + " " + Labels.Stringify() + "]";
        }

        public static string EscapXe(string s)
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
                        sb.Append("\n"); // not-escaped
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

        internal static void ResetIds()
        {
            IDGEN = 0;
        }*/
    }
}
