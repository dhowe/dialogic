using System;
using Dialogic;

namespace parser
{
    public class SuperParser
    {
        private readonly ChatRuntime runtime;

        internal SuperParser(ChatRuntime runtime)
        {
            this.runtime = runtime;
        }
    }
}
