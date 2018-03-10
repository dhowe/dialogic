using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    public class ParserTests
    {

        [Test]
        public void TestPrompts()
        {
            List<Chat> chats = ChatParser.ParseText("ASK Want a game?\nOPT Y #Game\n\nOPT N #End");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Ask)));
            Ask ask = (Dialogic.Ask)chats[0].commands[0];
            Assert.That(ask.Text, Is.EqualTo("Want a game?"));
            Assert.That(ask.Options().Count, Is.EqualTo(2));

            var options = ask.Options();
            Assert.That(options[0].GetType(), Is.EqualTo(typeof(Opt)));
            Assert.That(options[0].Text, Is.EqualTo("Y"));
            Assert.That(options[0].action.GetType(), Is.EqualTo(typeof(Go)));
            Assert.That(options[1].GetType(), Is.EqualTo(typeof(Opt)));
            Assert.That(options[1].Text, Is.EqualTo("N"));
            Assert.That(options[1].action.GetType(), Is.EqualTo(typeof(Go)));
        }


        [Test]
        public void TestComments()
        {
            List<Chat> chats = ChatParser.ParseText("SAY Thank you\n//SAY Hello\nAnd Goodbye");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(2));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[1].Text, Is.EqualTo("And Goodbye"));
            Assert.That(chats[0].commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you\n//SAY Hello\n// And Goodbye");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("//\n//SAY Thank you\n//SAY Hello\n// And Goodbye");
            Assert.That(chats.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestFindSoft()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("FIND {num=1}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.Null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].GetMeta("num"), Is.Not.Null);
            var meta = chats[0].commands[0].GetMeta("num");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            Constraint constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));

            chats = ChatParser.ParseText("FIND {a*=(hot|cool)}");
            var find = chats[0].commands[0];
            Assert.That(find.GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].Text, Is.Null);
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.Not.Null);
            meta = chats[0].commands[0].GetMeta("a");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));

            chats = ChatParser.ParseText("FIND {do=1}");
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            var finder = chats[0].commands[0];
            Assert.That(finder.GetType(), Is.EqualTo(typeof(Find)));
            meta = chats[0].commands[0].GetMeta("do");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(false));
        }

        [Test]
        public void TestFindHard()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("FIND {!num=1}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.Null);
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].GetMeta("num"), Is.Not.Null);
            var meta = chats[0].commands[0].GetMeta("num");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            Constraint constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(true));

            chats = ChatParser.ParseText("FIND {!a*=(hot|cool)}");
            var find = chats[0].commands[0];
            Assert.That(find.GetType(), Is.EqualTo(typeof(Find)));
            Assert.That(chats[0].commands[0].Text, Is.Null);
            Assert.That(chats[0].commands[0].GetMeta("a"), Is.Not.Null);
            meta = chats[0].commands[0].GetMeta("a");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(true));

            chats = ChatParser.ParseText("FIND {!do=1}");
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            var finder = chats[0].commands[0];
            Assert.That(finder.GetType(), Is.EqualTo(typeof(Find)));
            meta = chats[0].commands[0].GetMeta("do");
            Assert.That(meta.GetType(), Is.EqualTo(typeof(Constraint)));
            constraint = (Dialogic.Constraint)meta;
            Assert.That(constraint.IsStrict(), Is.EqualTo(true));
        }

        [Test]
        public void TestCommands()
        {
            List<Chat> chats;

            chats = ChatParser.ParseText("GO #Twirl");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Twirl"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));

            chats = ChatParser.ParseText("GO Twirl");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Twirl"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Go)));

            chats = ChatParser.ParseText("DO #Twirl");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Twirl"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));

            chats = ChatParser.ParseText("DO Twirl");
            //Console.WriteLine(chats[0].ToTree());
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Twirl"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Do)));

            chats = ChatParser.ParseText("SAY Thank you");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you\n \t\nAnd Goodbye");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(2));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[1].Text, Is.EqualTo("And Goodbye"));
            Assert.That(chats[0].commands[1].GetType(), Is.EqualTo(typeof(Say)));

            chats = ChatParser.ParseText("SAY Thank you { pace = fast}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));

            chats = ChatParser.ParseText("SAY Thank you {pace=fast,count=2}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].HasMeta(), Is.EqualTo(true));
            Assert.That(chats[0].commands[0].GetMeta("pace"), Is.EqualTo("fast"));
            Assert.That(chats[0].commands[0].GetMeta("count"), Is.EqualTo("2"));

            chats = ChatParser.ParseText("SAY Thank you {}");
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(chats[0].commands[0].Text, Is.EqualTo("Thank you"));
            Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Say)));
            Assert.That(chats[0].commands[0].HasMeta(), Is.EqualTo(false));

            string[] lines = {
                "DO #Twirl", "DO #Twirl {speed= fast}", "SAY Thank you", "WAIT", "WAIT .5",  "WAIT .5 {a=b}",
                "SAY Thank you {pace=fast,count=2}", "SAY Thank you", "FIND { num > 1, an != 4 }",
                "SAY Thank you { pace = fast}", "SAY Thank you {}", "Thank you"
            };
            var parser = new ChatParser();
            for (int i = 0; i < lines.Length; i++)
            {
                Command c = ChatParser.ParseText(lines[i])[0].commands[0];
                Assert.That(c is Command);
            }
        }

        [Test]
        public void TestGrammars()
        {
            List<Chat> chats = ChatParser.ParseText("GRAM { start: 'The <item>', item: cat }");
            Command gram = chats[0].commands[0];
            //Console.WriteLine(gram);
            //Assert.That(chats.Count, Is.EqualTo(11111));
            Assert.That(chats.Count, Is.EqualTo(1));
            Assert.That(chats[0].Count, Is.EqualTo(1));
            Assert.That(chats[0].GetType(), Is.EqualTo(typeof(Chat)));
            Assert.That(gram.Text, Is.Null);
            Assert.That(gram.GetType(), Is.EqualTo(typeof(Gram)));
            //new ChatRuntime(chats).Run();
        }

        [Test]
        public void TestExceptions()
        {
            Assert.Throws<ParseException>(() => ChatParser.ParseText("SAY"));
            //Assert.Throws<ParseException>(() => ChatParser.ParseText("GO Twirl")); // allowed
            //Assert.Throws<ParseException>(() => ChatParser.ParseText("DO Flip")); // allowed
            Assert.Throws<ParseException>(() => ChatParser.ParseText("CHAT Two Words"));
            Assert.Throws<ParseException>(() => ChatParser.ParseText("FIND {a = (b|c)}"));
        }
    }
}
