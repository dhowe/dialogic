using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Dialogic
{
    public class ScriptingEngine
    {
        private ScriptState<object> scriptState = null;

        public ScriptingEngine() {
            
        }

        public ScriptingEngine(Dictionary<string, object> globals) {
            foreach (var k in globals.Keys)
            {
                var code = "var " + k + " = \"" + globals[k].ToString() + "\";";
                System.Console.WriteLine(code);
                Exec(code);
            }
            Console.WriteLine(Exec("return animal;"));
            //Console.WriteLine($"scriptState={scriptState}");
        } 

        public static string Eval(string code)
        {
            return new ScriptingEngine().Exec(code);
        }

        public string Exec(string code)
        {
            scriptState = (scriptState == null) ? 
                CSharpScript.RunAsync(code).Result : 
                scriptState.ContinueWithAsync(code).Result;

            if (scriptState.ReturnValue != null)
            {
                var result = scriptState.ReturnValue.ToString();
                if (!string.IsNullOrEmpty(result)) return result;
            }

            return "";
        }

        public static void MainOff(string[] args)
        {
            //int val = Util.ToNullable<int>();
            var strAmount = "3.3";
            int? i = strAmount.GetValueOrNull<int>();
            Console.WriteLine("i="+i);
            double? d = strAmount.GetValueOrNull<double>();
            Console.WriteLine("d="+d);

            //decimal? amount = strAmount.GetValueOrNull<decimal>();

            /*Console.WriteLine($"Hello");
            Dictionary<string, object> globals = new Dictionary<string, object>() {
                    { "animal", "dog" },
                    { "place", "Istanbul" },
                    { "Happy", "HappyFlip" },
                    { "prep", "then" },
                    { "i", 0 }
            };
            var se = new ScriptingEngine(globals); */
        }
    }

 
}

