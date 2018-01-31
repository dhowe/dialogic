using System;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Dialogic
{
    public class ScriptingEngine
    {
        private ScriptState<object> scriptState = null;

        public ScriptingEngine() {
            
        } 

        public ScriptingEngine(ScriptState<object> o) {
            scriptState = o;
        } 

        public string Execute(string code)
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
    }
}

