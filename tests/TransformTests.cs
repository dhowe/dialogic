    using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Dialogic
{
	[TestFixture]
	class TransformTests : GenericTests
	{
        [Test]
        public void PartialTransformIssue()
        {
            ChatRuntime rt = new ChatRuntime();

            rt.ParseText("SET $test = (a) (b)\nSAY $test.Cap()", true);
            Assert.That(rt.InvokeImmediate(globals), Is.EqualTo("A b"));

            Resolver.DBUG = false;

            rt = new ChatRuntime();
            rt.ParseText("SET $test = (a | a) (b | b)\nSAY $test.Cap()", true);
            Assert.That(rt.InvokeImmediate(globals), Is.EqualTo("A b"));
        }

        [Test]
        public void TransformIssue()
        {
            string res;

            res = new Resolver(null).Bind("(ab).Cap()", CreateParentChat("c"), null);
            //Console.WriteLine("1: " + res);
            Assert.That(res, Is.EqualTo("Ab"));

            res = new Resolver(null).Bind("(a b).Cap()", CreateParentChat("c"), null);
            ///Console.WriteLine("2: " + res);
            Assert.That(res, Is.EqualTo("A b"));


            Resolver.DBUG = false;
            res = new Resolver(null).Bind("((a b)).Cap()", CreateParentChat("c"), null);
            //Console.WriteLine("3: " + res);
            Assert.That(res, Is.EqualTo("A b"));

            res = new Resolver(null).Bind("((a) (b)).Cap()", CreateParentChat("c"), null);
            //Console.WriteLine("4: " + res);
            Assert.That(res, Is.EqualTo("A b"));

            res = new Resolver(null).Bind("((a)(b)).Cap()", CreateParentChat("c"), null);
            //Console.WriteLine("5: " + res);
            Assert.That(res, Is.EqualTo("Ab"));
        }

        [Test]
        public void ParseMultiTransforms()
        {
            Chat c = CreateParentChat("c1");
            List<TxForm> txs;

            txs = TxForm.Parse("(a).Cap() (b).Cap()", c);
            Assert.That(txs.Count, Is.EqualTo(2));
            Assert.That(txs[0].text, Is.EqualTo("(a).Cap()"));
            Assert.That(txs[0].content, Is.EqualTo("a"));
            Assert.That(txs[0].transformText, Is.EqualTo(".Cap()"));

            Assert.That(txs[1].text, Is.EqualTo("(b).Cap()"));
            Assert.That(txs[1].content, Is.EqualTo("b"));
            Assert.That(txs[1].transformText, Is.EqualTo(".Cap()"));


            txs = TxForm.Parse("((a).Cap()).Cap()", c);

            Assert.That(txs.Count, Is.EqualTo(2));
            Assert.That(txs[0].text, Is.EqualTo("(a).Cap()"));
            Assert.That(txs[0].content, Is.EqualTo("a"));
            Assert.That(txs[0].transformText, Is.EqualTo(".Cap()"));

            Assert.That(txs[1].text, Is.EqualTo("((a).Cap()).Cap()"));
            Assert.That(txs[1].content, Is.EqualTo("(a).Cap()"));
            Assert.That(txs[1].transformText, Is.EqualTo(".Cap()"));


            txs = TxForm.Parse("(((a).Cap()).Cap()).Cap()", c);
            //Console.WriteLine(txs.Stringify());

            Assert.That(txs.Count, Is.EqualTo(3));
            Assert.That(txs[0].text, Is.EqualTo("(a).Cap()"));
            Assert.That(txs[0].content, Is.EqualTo("a"));
            Assert.That(txs[0].transformText, Is.EqualTo(".Cap()"));

            Assert.That(txs[1].text, Is.EqualTo("((a).Cap()).Cap()"));
            Assert.That(txs[1].content, Is.EqualTo("(a).Cap()"));
            Assert.That(txs[1].transformText, Is.EqualTo(".Cap()"));

            Assert.That(txs[2].text, Is.EqualTo("(((a).Cap()).Cap()).Cap()"));
            Assert.That(txs[2].content, Is.EqualTo("((a).Cap()).Cap()"));
            Assert.That(txs[2].transformText, Is.EqualTo(".Cap()"));
        }

        [Test]
        public void ParseTransforms()
        {
            Chat c = CreateParentChat("c1");
            TxForm tx;

            tx = TxForm.Parse("(a).Cap()", c)[0];
            Assert.That(tx.text, Is.EqualTo("(a).Cap()"));
            Assert.That(tx.content, Is.EqualTo("a"));
            Assert.That(tx.transformText, Is.EqualTo(".Cap()"));

            tx = TxForm.Parse("(a b).Cap()", c)[0];
            Assert.That(tx.text, Is.EqualTo("(a b).Cap()"));
            Assert.That(tx.content, Is.EqualTo("a b"));
            Assert.That(tx.transformText, Is.EqualTo(".Cap()"));

            tx = TxForm.Parse("((a b)).Cap()", c)[0];
            Assert.That(tx.text, Is.EqualTo("((a b)).Cap()"));
            Assert.That(tx.content, Is.EqualTo("a b"));
            Assert.That(tx.transformText, Is.EqualTo(".Cap()"));

            tx = TxForm.Parse("((a) (b)).Cap()", c)[0];
            Assert.That(tx.text, Is.EqualTo("((a) (b)).Cap()"));
            Assert.That(tx.content, Is.EqualTo("a b"));
            Assert.That(tx.transformText, Is.EqualTo(".Cap()"));

            tx = TxForm.Parse("((a)(b)).Cap()", c)[0];
            Assert.That(tx.text, Is.EqualTo("((a)(b)).Cap()"));
            Assert.That(tx.content, Is.EqualTo("ab"));
            Assert.That(tx.transformText, Is.EqualTo(".Cap()"));

            tx = TxForm.Parse("((ab)).Cap()", c)[0];
            Assert.That(tx.text, Is.EqualTo("((ab)).Cap()"));
            Assert.That(tx.content, Is.EqualTo("ab"));
            Assert.That(tx.transformText, Is.EqualTo(".Cap()"));

            tx = TxForm.Parse("(((ab)).Cap()", c)[0];
            Assert.That(tx.text, Is.EqualTo("((ab)).Cap()"));
            Assert.That(tx.content, Is.EqualTo("ab"));
            Assert.That(tx.transformText, Is.EqualTo(".Cap()"));

            tx = TxForm.Parse("((((ab)).Cap()))", c)[0];
            Assert.That(tx.text, Is.EqualTo("((ab)).Cap()"));
            Assert.That(tx.content, Is.EqualTo("ab"));
            Assert.That(tx.transformText, Is.EqualTo(".Cap()"));

            tx = TxForm.Parse(")((ab)).Cap()", c)[0];
            Assert.That(tx.text, Is.EqualTo("((ab)).Cap()"));
            Assert.That(tx.content, Is.EqualTo("ab"));
            Assert.That(tx.transformText, Is.EqualTo(".Cap()"));
        }

        [Test]
        public void TransformIssues()
        {
            ChatRuntime rt;
            string txt;
            Say say;
            Chat chat;

            Resolver.DBUG = false;

            txt = "SET $x = (a|a|a|a)\nSET test = (ok $x.Cap() | ok $x.Cap())\nSAY ($test).Cap()";
            rt = new ChatRuntime();
            rt.ParseText(txt);
            chat = rt.Chats().First();
            say = (Say)chat.commands[2];
            chat.Resolve(globals);
            //Console.WriteLine(res);
            Assert.That(say.Text(), Is.EqualTo("Ok A"));

            txt = "SET $x = (a|a|a|a)\nSET test = (ok $x.Cap() | ok $x.Cap())\nSAY $test.Cap()";
            rt = new ChatRuntime();
            rt.ParseText(txt);
            chat = rt.Chats().First();
            say = (Say)chat.commands[2];
            chat.Resolve(globals);
            //Console.WriteLine(res);
            Assert.That(say.Text(), Is.EqualTo("Ok A"));

            txt = "SAY (ok (a).Cap()).Cap()";
            rt = new ChatRuntime();
            rt.ParseText(txt);
            chat = rt.Chats().First();
            say = (Say)chat.commands[0];
            chat.Resolve(globals);
            //Console.WriteLine(res);
            Assert.That(say.Text(), Is.EqualTo("Ok A"));


            /////////////////////////////////////////////////////////////////

            txt = "SET $thing1 = (cat | cat)\nSAY A $thing1, many $thing1.Pluralize()";
            rt = new ChatRuntime();
            rt.ParseText(txt);
            chat = rt.Chats().First();
            say = (Say)chat.commands[1];
            chat.Resolve(globals);
            //Console.WriteLine(res);
            Assert.That(say.Text(), Is.EqualTo("A cat, many cats"));

            txt = "SET $thing1 = (cat | cat | cat)\nSAY A $thing1 $thing1";
            rt = new ChatRuntime();
            rt.ParseText(txt);
            chat = rt.Chats().First();
            say = (Say)chat.commands[1];
            chat.Resolve(globals);
            Assert.That(say.Text(), Is.EqualTo("A cat cat"));


            txt = "SET $thing1 = (cat | crow | cow)\nSAY A [save=$thing1], many $save.Pluralize()";
            rt = new ChatRuntime();
            rt.ParseText(txt);
            chat = rt.Chats().First();
            say = (Say)chat.commands[1];
            chat.Resolve(globals);
            Assert.That(say.Text(), Is.EqualTo("A cat, many cats").Or.EqualTo("A crow, many crows").Or.EqualTo("A cow, many cows"));
        }

        [Test]
        public void SimpleTransformResolveOnly()
        {
            string result, text;
            TxForm tran;
            Chat c1 = null;

            text = "The so-called (dog).Quotify() ran.";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
            Assert.That(result.ToString(), Is.EqualTo("&quot;dog&quot;"));

            text = "The (dog).Cap() ran.";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
            Assert.That(result.ToString(), Is.EqualTo("Dog"));

            text = "(ant).Articlize().Cap() ran.";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
            Assert.That(result.ToString(), Is.EqualTo("An ant"));

            text = "Today (ant).Cap().Articlize() ran.";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
            Assert.That(result.ToString(), Is.EqualTo("an Ant"));

            text = "Today (an ant).Cap() ran.";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
            Assert.That(result.ToString(), Is.EqualTo("An ant"));

            text = "(a (then).Cap())";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
            Assert.That(result.ToString(), Is.EqualTo("Then"));
        }

		[Test]
		public void SimpleTransformResolution()
		{   
			string result, text;
			TxForm tran;
			Chat c1 = null;

			text = "The so-called (dog).Quotify() ran.";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
			Assert.That(result.ToString(), Is.EqualTo("&quot;dog&quot;"));
            result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("The so-called &quot;dog&quot; ran."));

			text = "The (dog).Cap() ran.";
			tran = TxForm.Parse(text, c1)[0];         
			result = tran.Resolve();
			Assert.That(result.ToString(), Is.EqualTo("Dog"));
			result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("The Dog ran."));

			text = "(ant).Articlize().Cap() ran.";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
			Assert.That(result.ToString(), Is.EqualTo("An ant"));
            result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("An ant ran."));

			text = "Today (ant).Cap().Articlize() ran.";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
            Assert.That(result.ToString(), Is.EqualTo("an Ant"));
            result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("Today an Ant ran."));

			text = "Today (an ant).Cap() ran.";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
			Assert.That(result.ToString(), Is.EqualTo("An ant"));
            result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("Today An ant ran."));

			text = "(a (then).Cap())";
            tran = TxForm.Parse(text, c1)[0];
            result = tran.Resolve();
            Assert.That(result.ToString(), Is.EqualTo("Then"));
            result = tran.Replace(text, result);
			Assert.That(result.ToString(), Is.EqualTo("(a Then)"));
		}

        [Test]
        public void TestPluralizePhrase()
        {
            Assert.That(Transforms.Pluralize("coffee cup"), Is.EqualTo("coffee cups"));
            Assert.That(Transforms.Pluralize("front tooth"), Is.EqualTo("front teeth"));
        }

        [Test]
        public void MultiTransformResolution()
        {
            var str = "(well|well).cap() don't you look (anger).emoadj().";
            var rt = new ChatRuntime(Client.AppConfig.TAC);
            rt.ParseText(str);
            //rt.strictMode = false;
            var s = rt.InvokeImmediate(globals);
            //Console.WriteLine(s);
            Assert.That(s.StartsWith("Well don't you look ", Util.IC), Is.True);
            Assert.That(s.Contains("anger"), Is.False);
            Assert.That(s.Contains("emoadj"), Is.False);

            str = "The (dog|).Cap() ran.";
            rt = new ChatRuntime(Client.AppConfig.TAC);
            rt.ParseText(str);

            //rt.strictMode = false;
            for (int i = 0; i < 5; i++)
            {
                s = rt.InvokeImmediate(globals);
                //Console.WriteLine(i + ") " + s);
                Assert.That(s.IsOneOf("The Dog ran.", "The ran."));
            }
        }

		[Test]
        public void TestPluralize()
        {
            Assert.That(Transforms.Pluralize(""), Is.EqualTo(""));
            Assert.That(Transforms.Pluralize("dog"), Is.EqualTo("dogs"));
            Assert.That(Transforms.Pluralize("eye"), Is.EqualTo("eyes"));

            Assert.That(Transforms.Pluralize("blonde"), Is.EqualTo("blondes"));
            Assert.That(Transforms.Pluralize("blond"), Is.EqualTo("blondes"));
            Assert.That(Transforms.Pluralize("foot"), Is.EqualTo("feet"));
            Assert.That(Transforms.Pluralize("man"), Is.EqualTo("men"));
            Assert.That(Transforms.Pluralize("tooth"), Is.EqualTo("teeth"));
            Assert.That(Transforms.Pluralize("cake"), Is.EqualTo("cakes"));
            Assert.That(Transforms.Pluralize("kiss"), Is.EqualTo("kisses"));
            Assert.That(Transforms.Pluralize("child"), Is.EqualTo("children"));

            Assert.That(Transforms.Pluralize("louse"), Is.EqualTo("lice"));

            Assert.That(Transforms.Pluralize("sheep"), Is.EqualTo("sheep"));
            Assert.That(Transforms.Pluralize("shrimp"), Is.EqualTo("shrimps"));
            Assert.That(Transforms.Pluralize("series"), Is.EqualTo("series"));
            Assert.That(Transforms.Pluralize("mouse"), Is.EqualTo("mice"));

            Assert.That(Transforms.Pluralize("beautiful"), Is.EqualTo("beautifuls"));

            Assert.That(Transforms.Pluralize("crisis"), Is.EqualTo("crises"));
            Assert.That(Transforms.Pluralize("thesis"), Is.EqualTo("theses"));
            Assert.That(Transforms.Pluralize("apothesis"), Is.EqualTo("apotheses"));
            Assert.That(Transforms.Pluralize("stimulus"), Is.EqualTo("stimuli"));
            Assert.That(Transforms.Pluralize("alumnus"), Is.EqualTo("alumni"));
            Assert.That(Transforms.Pluralize("corpus"), Is.EqualTo("corpora"));

            Assert.That(Transforms.Pluralize("woman"), Is.EqualTo("women"));
            Assert.That(Transforms.Pluralize("man"), Is.EqualTo("men"));
            Assert.That(Transforms.Pluralize("congressman"), Is.EqualTo("congressmen"));
            Assert.That(Transforms.Pluralize("alderman"), Is.EqualTo("aldermen"));
            Assert.That(Transforms.Pluralize("freshman"), Is.EqualTo("freshmen"));

            Assert.That(Transforms.Pluralize("bikini"), Is.EqualTo("bikinis"));
            Assert.That(Transforms.Pluralize("martini"), Is.EqualTo("martinis"));
            Assert.That(Transforms.Pluralize("menu"), Is.EqualTo("menus"));
            Assert.That(Transforms.Pluralize("guru"), Is.EqualTo("gurus"));

            Assert.That(Transforms.Pluralize("medium"), Is.EqualTo("media"));
            Assert.That(Transforms.Pluralize("concerto"), Is.EqualTo("concerti"));
            Assert.That(Transforms.Pluralize("terminus"), Is.EqualTo("termini"));

            Assert.That(Transforms.Pluralize("aquatics"), Is.EqualTo("aquatics"));
            Assert.That(Transforms.Pluralize("mechanics"), Is.EqualTo("mechanics"));

            Assert.That(Transforms.Pluralize("tomato"), Is.EqualTo("tomatoes"));
            Assert.That(Transforms.Pluralize("toe"), Is.EqualTo("toes"));

            Assert.That(Transforms.Pluralize("deer"), Is.EqualTo("deer"));
            Assert.That(Transforms.Pluralize("moose"), Is.EqualTo("moose"));
            Assert.That(Transforms.Pluralize("ox"), Is.EqualTo("oxen"));

            Assert.That(Transforms.Pluralize("tobacco"), Is.EqualTo("tobacco"));
            Assert.That(Transforms.Pluralize("cargo"), Is.EqualTo("cargo"));
            Assert.That(Transforms.Pluralize("golf"), Is.EqualTo("golf"));
            Assert.That(Transforms.Pluralize("grief"), Is.EqualTo("grief"));
            Assert.That(Transforms.Pluralize("wildlife"), Is.EqualTo("wildlife"));
            Assert.That(Transforms.Pluralize("taxi"), Is.EqualTo("taxis"));
            Assert.That(Transforms.Pluralize("Chinese"), Is.EqualTo("Chinese"));
            Assert.That(Transforms.Pluralize("bonsai"), Is.EqualTo("bonsai"));

            Assert.That(Transforms.Pluralize("gas"), Is.EqualTo("gases"));
            Assert.That(Transforms.Pluralize("bus"), Is.EqualTo("buses"));
        }
	}
}