using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Dialogic;

namespace tests
{
    [TestFixture]
    public class SerializeTests
    {
        const bool NO_VALIDATORS = true;

        public static IDictionary<string, object> globals
             = new Dictionary<string, object>() {
                { "obj-prop", "dog" },
                { "animal", "dog" },
                { "prep", "then" },
                { "group", "(a|b)" },
                { "cmplx", "($group | $prep)" },
                { "count", 4 }
         };

        [Test]
        public void SaveRestoreChats()
        {
            var rt = new ChatRuntime(AppConfig.Actors);
            rt.ParseFile(srcpath + "/data/noglobal.gs");
            Console.WriteLine(rt);
            var state = GameState.Create(rt);
            //dialogic.Run("#GScriptTest");

            var bytes = MessagePackSerializer.Serialize(state);
            var json = MessagePackSerializer.ToJson(bytes);
            //Console.WriteLine(json);

            var runtime = new ChatRuntime(AppConfig.Actors);
            MessagePackSerializer.Deserialize<GameState>(bytes).Update(runtime);
            Console.WriteLine(runtime);
        }
    }
}
