using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Dialogic
{
	[TestFixture]
	class GrammarTests : GenericTests
	{
		[Test]
		public void ResolveMultiwordCaps()
		{
			string[] lines;
			ChatRuntime runtime;
			Chat chat;
			string res;

			Resolver.DBUG = false;
			lines = new[] {
				"SET start = $A $B",
				"SET A=hello",
				"SET B=world",
				"SAY $start.Capitalize()",
			};
			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(null);
			res = chat.commands.Last().Text();
			//Console.WriteLine("OUT: " + res);
			Assert.That(res, Is.EqualTo("Hello world"));

			Resolver.DBUG = false;
			lines = new[] {
				"SET start = $A $B",
				"SET A=amazing",
				"SET B=world",
				"SAY $start.Articlize().Capitalize()",
			};
			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(null);
			res = chat.commands.Last().Text();
			//Console.WriteLine("OUT: " + res);
			Assert.That(res, Is.EqualTo("An amazing world"));

			//Resolver.DBUG = true;
			lines = new[] {
				"SET start = ($A $B | $A $B)",
				"SET A=hello",
				"SET B=world",
				"SAY $start.Capitalize()",
			};
			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(null);
			res = chat.commands.Last().Text();
			//Console.WriteLine("OUT: " + res);
			Assert.That(res, Is.EqualTo("Hello world"));
		}

		[Test]
		public void SaveGlobalResolveState()
		{
			string[] lines;
			ChatRuntime runtime;
			Chat chat;
			string res;

			lines = new[] {
				"SAY A girl [select1=$fish.name] $select1",
			};
			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(globals);
			res = chat.commands[0].Text();
			Assert.That(res, Is.EqualTo("A girl Fred Fred"));

			lines = new[] {
				"SAY A girl [select3=$fish.name.ToUpper()] $select3",
			};
			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(globals);
			res = chat.commands[0].Text();
			Assert.That(res, Is.EqualTo("A girl FRED FRED"));
		}


		[Test]
		public void ReplaceParensGrouping()
		{
			var lines = new[] {
				"CHAT c1",
				"SET $start = $aa $bb",
				"SET $aa = Word",
				"SET $bb = A B (C | D) | A B (C | D)",
				"SAY $start"
			};

			ChatRuntime runtime;
			Chat chat;
			string res = null;

			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime["c1"];
			chat.Resolve(globals);
			//Console.WriteLine("start="+globals["start"]);
			//Console.WriteLine("aa="+globals["aa"]);
			//Console.WriteLine("bb=" + globals["bb"]);
			for (int i = 0; i < 5; i++)
			{
				res = runtime.InvokeImmediate(globals);
				//Console.WriteLine(i + ") " + res);
				Assert.That(res, Is.EqualTo("Word A B C").Or.EqualTo("Word A B D"));
			}

		}

		[Test]
		public void TransformAliasTest()
		{
			string[] lines;
			ChatRuntime runtime;
			Chat chat;
			string res;
			lines = new[] {
				"SAY A girl [select2=$fish.name] $select2.ToUpper()",
			};
			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(globals);
			res = chat.commands[0].Text();
			Assert.That(res, Is.EqualTo("A girl Fred FRED"));
		}

		[Test]
		public void ResolveWithAlias()
		{
			var globs = new Dictionary<string, object> {
				{ "obj-prop", "dog" },
				{ "animal", "dog" },
				{ "prep", "then" },
				{ "group", "(a|b)" },
				{ "cmplx", "($group | $prep)" },
				{ "count", 4 },
				{ "fish",  new Fish("Fred")}
			};

			string[] lines;
			ChatRuntime runtime;
			Chat chat;
			string res;

			lines = new[] {
				"SAY The girl was $fish.Id().",
			};
			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			//Console.WriteLine(chat.ToTree());
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(globals);
			res = chat.commands[0].Text();
			Assert.That(res, Is.EqualTo("The girl was 9."));


			//Resolver.DBUG = true;
			lines = new[] {
				"SAY A girl [selected=$fish.Id()] $selected",
			};
			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(globals);
			res = chat.commands[0].Text();
			Assert.That(res, Is.EqualTo("A girl 9 9"));
		}

		[Test]
		public void ResolveWithCustomTransform()
		{
			string[] lines;
			ChatRuntime runtime;
			Chat chat;
			string res;

			lines = new[] {
				"SET hero = artist",
				"SAY She was an $hero.capify().",
			};
			runtime = new ChatRuntime();
			runtime.AddTransform("capify", _AllCaps);
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(null);
			res = chat.commands[1].Text();
			//Console.WriteLine(res);
			Assert.That(res, Is.EqualTo("She was an ARTIST."));
		}

		[Test]
		public void SimpleSetExpansions()
		{
			string[] lines;
			string text;
			Chat chat;

			// local
			lines = new[] {
				"CHAT c1",
				"SET ab = hello",
				"SAY $ab",
			};
			text = String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat.commands[1].Text(), Is.EqualTo("hello"));

			// global-miss
			lines = new[] {
				"CHAT c1",
				"SET ab = hello",
				"SAY $ab",
			};
			text = String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat.commands[1].Text(), Is.EqualTo("hello"));

			// global-hit
			lines = new[] {
				"CHAT c1",
				"SAY $animal",
			};
			text = String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat.commands[0].Text(), Is.EqualTo("dog"));

			// global-properties
			lines = new[] {
				"CHAT c1",
				"SAY $fish.name",
			};
			text = String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat.commands[0].Text(), Is.EqualTo("Fred"));

			// global-bounded
			lines = new[] {
				"CHAT c1",
				"SAY ${fish.name}",
			};
			text = String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat.commands[0].Text(), Is.EqualTo("Fred"));

			// global-nested
			lines = new[] {
				"CHAT c1",
				"SAY $fish.flipper.speed",
			};
			text = String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat.commands[0].Text(), Is.EqualTo("1.1"));

			// global-nested-bounded
			lines = new[] {
				"CHAT c1",
				"SAY ${fish.flipper.speed}",
			};
			text = String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat.commands[0].Text(), Is.EqualTo("1.1"));

			// cross-chat-global
			lines = new[] {
				"CHAT c1",
				"SET $c1_ab = hello",
				"CHAT c2",
				"SAY $c1_ab",
			};
			text = String.Join("\n", lines);
			var chats = ChatParser.ParseText(text, true);
			chats.ForEach(c => c.Resolve(globals));
			Assert.That(chats[1].commands[0].Text(), Is.EqualTo("hello"));


			return; // TODO: add chats to globals, remove special-case code 

			// chat-direct access
			lines = new[] {
				"CHAT c1",
				"SET foo=bar",
				"CHAT c2",
				"SAY $chats.c1.foo",
			};
			text = String.Join("\n", lines);
			chats = ChatParser.ParseText(text, true);
			chats.ForEach(c => c.Resolve(globals));
			Assert.That(chats[1].commands[0].Text(), Is.EqualTo("bar"));

			// chat-direct bounded
			lines = new[] {
				"CHAT c1",
				"SET foo=bar",
				"CHAT c2",
				"SAY ${chats.c1.foo}",
			};
			text = String.Join("\n", lines);
			chats = ChatParser.ParseText(text, true);
			chats.ForEach(c => c.Resolve(globals));
			Assert.That(chats[1].commands[0].Text(), Is.EqualTo("bar"));
		}

		[Test]
		public void ResolveWithCustomChainedString()
		{
			string[] lines;
			ChatRuntime runtime;
			Chat chat;
			string res;

			lines = new[] {
				"SET hero = artist",
				"SAY She was $hero.articlize().ToUpper().",
			};
			runtime = new ChatRuntime();
			runtime.AddTransform("capify", _AllCaps);
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(null);
			res = chat.commands[1].Text();
			//Console.WriteLine(res);
			Assert.That(res, Is.EqualTo("She was AN ARTIST."));
		}

		[Test]
		public void ResolveWithStringCustomChained()
		{
			string[] lines;
			ChatRuntime runtime;
			Chat chat;
			string res;

			lines = new[] {
				"SET hero = artist",
				"SAY She was $hero.ToUpper().articlize().",
			};
			runtime = new ChatRuntime();
			runtime.AddTransform("capify", _AllCaps);
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(null);
			res = chat.commands[1].Text();
			//Console.WriteLine(res);
			Assert.That(res, Is.EqualTo("She was an ARTIST."));
		}

		[Test]
		public void ResolveWithCustomChainedTransform()
		{
			string[] lines;
			ChatRuntime runtime;
			Chat chat;
			string res;

			lines = new[] {
				"SET hero = artist",
				"SAY She was $hero.articlize().capify().",
			};
			runtime = new ChatRuntime();
			runtime.AddTransform("capify", _AllCaps);
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(null);
			res = chat.commands[1].Text();
			//Console.WriteLine(res);
			Assert.That(res, Is.EqualTo("She was AN ARTIST."));


			lines = new[] {
				"SET hero = artist",
				"SAY She was $hero.capify().articlize().",
			};
			runtime = new ChatRuntime();
			runtime.AddTransform("capify", _AllCaps);
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(null);
			res = chat.commands[1].Text();
			//Console.WriteLine(res);
			Assert.That(res, Is.EqualTo("She was an ARTIST."));
		}

		[Test]
		public void ResolveWithArticlize()
		{
			string[] lines;
			ChatRuntime runtime;
			Chat chat;
			string res, last = null;

			lines = new[] {
				"SET hero = artist",
				"SAY She was $hero.articlize().",
			};
			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(null);
			res = chat.commands[1].Text();
			Assert.That(res, Is.EqualTo("She was an artist."));


			lines = new[] {
				"SET hero = (animal | artist | person | banker)",
				"SAY She was $hero.articlize().",
			};
			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			for (int i = 0; i < 15; i++)
			{
				chat.Resolve(null);
				res = chat.commands[1].Text();
				Assert.That(res, Is.Not.EqualTo(last));
				Assert.That(res, Is.EqualTo("She was an artist.").
								 Or.EqualTo("She was an animal.").
								 Or.EqualTo("She was a person.").
								 Or.EqualTo("She was a banker."));
				last = res;
			}
		}

		[Test]
		public void SaveResolveState()
		{
			string[] lines;
			ChatRuntime runtime;
			Chat chat;
			string res;


			lines = new[] {
				"SET hero = (Jane | Jill)",
				"SAY A girl [selected=$hero]&nbsp;",
				"SAY $selected"
			};
			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.commands[0].Resolve(null);
			Assert.That(chat.scope["hero"], Is.EqualTo("(Jane | Jill)"));
			chat.commands[1].Resolve(null);
			Assert.That(chat.commands[1].Text(), Is.EqualTo("A girl Jane ").Or.EqualTo("A girl Jill "));
			Assert.That(chat.scope["selected"], Is.EqualTo("Jane").Or.EqualTo("Jill"));
			chat.commands[2].Resolve(null);
			res = chat.commands[1].Text() + chat.commands[2].Text();
			Assert.That(res, Is.EqualTo("A girl Jane Jane").
							 Or.EqualTo("A girl Jill Jill"));

			lines = new[] {
				"SET hero = (Jane | Jill)",
				"SAY A girl [a=$hero]&nbsp;",
				"SAY $a"
			};
			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(null);
			res = chat.commands[1].Text() + chat.commands[2].Text();
			Assert.That(res, Is.EqualTo("A girl Jane Jane").
							 Or.EqualTo("A girl Jill Jill"));
			//Console.WriteLine("2: "+chat + " " + chat.scope.Stringify() 
			//+ "\nglobals=" + globals.Stringify());

			lines = new[] {
				"SET hero = (Jane | Jill)",
				"SAY A girl [selected=$hero]&nbsp;",
				"SAY $selected."
			};
			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(null);
			res = chat.commands[1].Text() + chat.commands[2].Text();
			Assert.That(res, Is.EqualTo("A girl Jane Jane.").
							 Or.EqualTo("A girl Jill Jill."));


			lines = new[] {
				"SET hero = (Jane | Jill)",
				"SAY A girl [selected=${hero}]&nbsp;",
				"SAY $selected."
			};
			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(null);
			res = chat.commands[1].Text() + chat.commands[2].Text();
			Assert.That(res, Is.EqualTo("A girl Jane Jane.").
							 Or.EqualTo("A girl Jill Jill."));

			lines = new[] {
				"SET hero = (Jane | Jill)",
				"SAY A girl [selected=$hero] $selected."
			};

			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(null);
			res = chat.commands[1].Text();// + chat.commands[2].Text();
			Assert.That(res, Is.EqualTo("A girl Jane Jane.").
										 Or.EqualTo("A girl Jill Jill."));

			lines = new[] {
				"SET hero = (Jane | Jill)",
				"SAY A girl [selected=${hero}] ${selected}."
			};

			runtime = new ChatRuntime();
			runtime.ParseText(string.Join("\n", lines));
			chat = runtime.Chats()[0];
			Assert.That(chat, Is.Not.Null);
			chat.Resolve(null);
			res = chat.commands[1].Text();// + chat.commands[2].Text();
			Assert.That(res, Is.EqualTo("A girl Jane Jane.").
									 Or.EqualTo("A girl Jill Jill."));
		}

		[Test]
		public void ResolveSubstringSymbols()
		{
			var lines = new[] {
				"CHAT wine1 {noStart=true}",
				"SET ant = $antelope",
				"SET antelope = C",
				"SAY $ant $antelope"
			};
			ChatRuntime runtime = new ChatRuntime(Tendar.AppConfig.Actors);
			runtime.ParseText(string.Join("\n", lines), false);
			var chat = runtime.Chats()[0];

			runtime.Chats().ForEach(c => c.Resolve(null));

			//Console.WriteLine(chat.ToTree() + "\n" + chat.locals.Stringify());

			Say say = (Dialogic.Say)runtime.Chats().Last().commands.Last();
			var result = say.Text();
			Assert.That(result, Is.EqualTo("C C"));
		}


		[Test]
		public void RepeatedExpand()
		{
			string[] lines = {
				"CHAT myGrammar {defaultCmd=SET}",
				"start = $subject $verb $object.",
				"subject = I | You | They",
				"object = coffee | bread | milk",
				"verb = want | hate | like | love",
				"SAY $start",

			};
			ChatRuntime rt = new ChatRuntime();
			rt.ParseText(String.Join("\n", lines));
			Chat chat = rt.Chats()[0];

			Say say = (Dialogic.Say)chat.commands.Last();

			chat.commands.ForEach(c => c.Resolve(globals));

			var results = new HashSet<string>();
			for (int i = 0; i < 10; i++)
			{
				results.Add((string)say.Resolve(globals)[Meta.TEXT]);
			}

			Assert.That(results.Count, Is.GreaterThan(1));
		}

		[Test]
		public void RepeatedSymbolTest()
		{
			string[] lines = {
				"CHAT myGrammar {defaultCmd=SET}",
				"start = $subject verb object.",
				"subject = I | You | They",
				"object = coffee | bread | milk",
				"verb = want | hate | like | love",
				"SAY $start $start $start $start $start $start $start $start",
			};
			ChatRuntime rt = new ChatRuntime();
			rt.ParseText(String.Join("\n", lines));

			rt.Chats()[0].commands.ForEach(c => c.Resolve(globals));
			var all = rt.Chats()[0].commands.Last().Text();
			var says = all.Split('.');

			var results = new HashSet<string>();
			for (int i = 0; i < says.Length; i++)
			{
				if (says[i].IsNullOrEmpty()) continue;
				results.Add(says[i].Trim());

			}

			Assert.That(results.Count, Is.GreaterThan(1));
		}


		[Test]
		public void SimpleSets()
		{
			Chat chat;
			Set set;

			chat = ChatParser.ParseText("CHAT c1\nSET $a = 4", NO_VALIDATORS)[0];
			Assert.That(chat, Is.Not.Null);
			Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
			set = (Dialogic.Set)chat.commands[0];
			Assert.That(set.text, Is.EqualTo("a"));
			Assert.That(set.op, Is.EqualTo(Assignment.EQ));
			Assert.That(set.value, Is.EqualTo("4"));
			set.Resolve(globals);
			object outv = null;
			chat.scope.TryGetValue("a", out outv);
			Assert.That(outv, Is.Null);
			Assert.That(globals["a"], Is.EqualTo("4"));
			globals.Remove("a");

			chat = ChatParser.ParseText("CHAT c1\nSET a =4", NO_VALIDATORS)[0];
			Assert.That(chat, Is.Not.Null);
			Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
			set = (Dialogic.Set)chat.commands[0];
			Assert.That(set.text, Is.EqualTo("a"));
			Assert.That(set.op, Is.EqualTo(Assignment.EQ));
			Assert.That(set.value, Is.EqualTo("4"));
			set.Resolve(globals);
			outv = null;
			globals.TryGetValue("a", out outv);
			Assert.That(outv, Is.Null);
			Assert.That(chat.scope["a"], Is.EqualTo("4"));
		}

		[Test]
		public void SimpleSetsWithVars()
		{
			Chat chat;
			Set set;

			chat = ChatParser.ParseText("CHAT c1\nSET a= $obj-prop", NO_VALIDATORS)[0];
			Assert.That(chat, Is.Not.Null);
			Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
			set = (Dialogic.Set)chat.commands[0];
			Assert.That(set.text, Is.EqualTo("a"));
			Assert.That(set.op, Is.EqualTo(Assignment.EQ));
			Assert.That(set.value, Is.EqualTo("$obj-prop"));
			set.Resolve(globals);
			Assert.That(chat.scope["a"], Is.EqualTo("$obj-prop"));


			chat = ChatParser.ParseText("CHAT c1\nSET a2 = $obj-prop", NO_VALIDATORS)[0];
			Assert.That(chat, Is.Not.Null);
			Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
			set = (Dialogic.Set)chat.commands[0];
			Assert.That(set.text, Is.EqualTo("a2"));
			Assert.That(set.op, Is.EqualTo(Assignment.EQ));
			Assert.That(set.value, Is.EqualTo("$obj-prop"));
			set.Resolve(globals);
			Assert.That(chat.scope["a2"], Is.EqualTo("$obj-prop"));

			chat = ChatParser.ParseText("CHAT c1\nSET a= ${obj-prop}", NO_VALIDATORS)[0];
			Assert.That(chat, Is.Not.Null);
			Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
			set = (Dialogic.Set)chat.commands[0];
			Assert.That(set.text, Is.EqualTo("a"));
			Assert.That(set.op, Is.EqualTo(Assignment.EQ));
			Assert.That(set.value, Is.EqualTo("${obj-prop}"));
			set.Resolve(globals);
			Assert.That(chat.scope["a"], Is.EqualTo("${obj-prop}"));


			chat = ChatParser.ParseText("CHAT c1\nSET a2 = ${obj-prop}", NO_VALIDATORS)[0];
			Assert.That(chat, Is.Not.Null);
			Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
			set = (Dialogic.Set)chat.commands[0];
			Assert.That(set.text, Is.EqualTo("a2"));
			Assert.That(set.op, Is.EqualTo(Assignment.EQ));
			Assert.That(set.value, Is.EqualTo("${obj-prop}"));
			set.Resolve(globals);
			Assert.That(chat.scope["a2"], Is.EqualTo("${obj-prop}"));
		}

		[Test]
		public void SimpleSetsWithOr()
		{
			Chat chat;
			Set set;

			chat = ChatParser.ParseText("CHAT c1\nSET $a = (4 | 5)", NO_VALIDATORS)[0];
			Assert.That(chat, Is.Not.Null);
			Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
			set = (Dialogic.Set)chat.commands[0];
			Assert.That(set.text, Is.EqualTo("a"));
			Assert.That(set.op, Is.EqualTo(Assignment.EQ));
			Assert.That(set.value, Is.EqualTo("(4 | 5)"));
			set.Resolve(globals);
			//Assert.That(globals["a"], Is.EqualTo("4").Or.EqualTo("5"));
			Assert.That(globals["a"], Is.EqualTo("(4 | 5)"));
			globals.Remove("a");

			chat = ChatParser.ParseText("CHAT c1\nSET a = ( 4 | 5 )", NO_VALIDATORS)[0];
			Assert.That(chat, Is.Not.Null);
			Assert.That(chat.commands[0].GetType(), Is.EqualTo(typeof(Set)));
			set = (Dialogic.Set)chat.commands[0];
			Assert.That(set.text, Is.EqualTo("a"));
			Assert.That(set.op, Is.EqualTo(Assignment.EQ));
			Assert.That(set.value, Is.EqualTo("( 4 | 5 )"));
			set.Resolve(globals);
			object outv = null;
			globals.TryGetValue("a", out outv);
			Assert.That(outv, Is.Null);
			Assert.That(chat.scope["a"], Is.EqualTo("( 4 | 5 )"));
		}

		[Test]
		public void SetFromExternal()
		{
			string[] lines = {
				"CHAT WineReview {type=a,stage=b}",
				"SET $review=$desc $fortune $ending",
				"SET $desc=You look tasty: gushing blackberry into the rind of day-old ennui.",
				"SET $fortune=Under your skin, tears undulate like a leaky eel.",
				"SET $ending=And thats the end of the story...",
				"CHAT External {type=a,stage=b}",
				"SAY $review",
			};
			ChatRuntime rt = new ChatRuntime(Tendar.AppConfig.Actors);
			rt.ParseText(String.Join("\n", lines));
			var chats = rt.Chats();
			//Console.WriteLine(rt);

			Chat chat1 = chats[0], chat2 = chats[1];
			Say say = (Dialogic.Say)chat2.commands.Last();

			chat1.commands.ForEach(c => c.Resolve(globals));
			chat2.commands.ForEach(c => c.Resolve(globals));

			Assert.That(say.Text(), Is.EqualTo("You look tasty: gushing blackberry into the rind of day-old ennui. Under your skin, tears undulate like a leaky eel. And thats the end of the story..."));
		}

		[Test]
		public void SetRules1()
		{
			string[] lines = {
				"CHAT WineReview {type=a,stage=b}",
				"SET review=$desc $fortune $ending",
				"SET desc=You look tasty: gushing blackberry into the rind of day-old ennui.",
				"SET fortune=Under your skin, tears undulate like a leaky eel.",
				"SET ending=And thats the end of the story...",
				"SAY $review",
			};
			var chat = ChatParser.ParseText(String.Join("\n", lines))[0];

			var last = chat.commands[chat.commands.Count - 1];
			Assert.That(last, Is.Not.Null);
			Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));

			chat.commands.ForEach(c => c.Resolve(globals));

			Assert.That(chat.scope.ContainsKey("review"), Is.True);
			Assert.That(chat.scope.ContainsKey("ending"), Is.True);

			Say say = (Dialogic.Say)last;
			//Console.WriteLine(chat.ToTree()+"\nSAY: "+say.Text());

			Assert.That(say.Text(), Is.EqualTo("You look tasty: gushing blackberry into the rind of day-old ennui. Under your skin, tears undulate like a leaky eel. And thats the end of the story..."));
		}

		/*[Test]
		public void SetRules2() // causing hangs in nunit tests
		{
			string[] lines = {
				"CHAT WineReview {type=a,stage=b}",
				"SET review=$desc $fortune $ending",
				"SET ending=($score | $end-phrase) Goodbye!",
				"SET desc=You look tasty: gushing blackberry into the rind of day-old ennui.",
				"SET score=The judges give that a 1.",
				"SET fortune=You will live a short life in poverty.",
				"SET end-phrase=And thats the end of the story...",
				"SAY $review",
			};
			var chat = ChatParser.ParseText(String.Join("\n", lines))[0];

			var last = chat.commands[chat.commands.Count - 1];
			Assert.That(last, Is.Not.Null);
			Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));

			chat.commands.ForEach(c => c.Resolve(globals));

			Assert.That(globals.ContainsKey("WineReview.review"), Is.False);
			Assert.That(globals.ContainsKey("WineReview.ending"), Is.False);
			Assert.That(chat.scope.ContainsKey("review"), Is.True);
			Assert.That(chat.scope.ContainsKey("ending"), Is.True);

			Say say = (Dialogic.Say)last;
			var text = say.Text();
			//Console.WriteLine("GOT: "+text);
			Assert.That(text.StartsWith("You look tasty: gushing", Util.IC), Is.True);
			Assert.That(text.EndsWith("Goodbye!", Util.IC), Is.True);
		}*/

		[Test]
		public void SetRules3()
		{
			string[] lines = {
				"CHAT WineReview {type=a,stage=b}",
				"SET review=$desc $fortune $ending",
				"SET ending=$score | $end-phrase",
				"SET desc=You look tasty: gushing blackberry into the rind of day-old ennui.",
				"SET score=The judges give that a 1; good luck with the poverty.",
				"SET fortune=You will live a short life with dismal hygiene.",
				"SET end-phrase=And thats the end of the story. Good luck with the poverty.",
				"SAY $review",
			};
			var chat = (Chat)ChatParser.ParseText(String.Join("\n", lines))[0];
			var last = chat.commands[chat.commands.Count - 1];


			Assert.That(last, Is.Not.Null);
			Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));

			chat.commands.ForEach(c => c.Resolve(globals));

			//Console.WriteLine(chat.AsGrammar(globals));
			//Console.WriteLine("------------------------------------");
			//Console.WriteLine(chat.ExpandNoGroups(globals,"review"));
			//Console.WriteLine("------------------------------------");

			Assert.That(globals.ContainsKey("WineReview.review"), Is.False);
			Assert.That(globals.ContainsKey("WineReview.ending"), Is.False);
			Assert.That(chat.scope.ContainsKey("review"), Is.True);
			Assert.That(chat.scope.ContainsKey("ending"), Is.True);

			Say say = (Dialogic.Say)last;

			for (int i = 0; i < 10; i++)
			{
				var text = (string)say.Resolve(globals)[Meta.TEXT];
				//Console.WriteLine(i+") "+text);
				Assert.That(text.StartsWith("You look tasty", Util.IC), Is.True);
				Assert.That(text.EndsWith("with the poverty.", Util.IC), Is.True);
			}
		}

		[Test]
		public void SetRulesWithOrs()
		{
			string[] lines = {
				"CHAT c1 {type=a,stage=b}",
				"SET review=$greeting",
				"SET greeting=(Hello | Goodbye)",
				"SAY $review",
			};
			var chat = ChatParser.ParseText(String.Join("\n", lines))[0];

			//Console.WriteLine(chat.ToTree());
			var last = chat.commands[chat.commands.Count - 1];
			Assert.That(last, Is.Not.Null);
			Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));

			chat.commands.ForEach(c => c.Resolve(globals));

			Assert.That(globals.ContainsKey("c1.review"), Is.False);
			Assert.That(globals.ContainsKey("c1.greeting"), Is.False);
			Assert.That(chat.scope.ContainsKey("review"), Is.True);
			Assert.That(chat.scope.ContainsKey("greeting"), Is.True);
			Say say = (Dialogic.Say)last;

			for (int i = 0; i < 10; i++)
			{
				say.Resolve(globals);
				//Console.WriteLine(say.Text());
				Assert.That(say.Text(), Is.EqualTo("Hello").Or.EqualTo("Goodbye"));
			}
		}

		[Test]
		public void SetDefaultCommand()
		{
			string[] lines = {
				"CHAT c1 {type=a,stage=b,defaultCmd=SET}",
				"SET review=$greeting",
				"greeting=(Hello | Goodbye)",
				"SAY $review",
			};

			var chat = ChatParser.ParseText(String.Join("\n", lines))[0];
			//Console.WriteLine(chat.ToTree());

			for (int i = 0; i < 10; i++)
			{
				chat.commands.ForEach(c => c.Resolve(globals));
				Assert.That(((Dialogic.Say)chat.commands.Last()).Text(),
					Is.EqualTo("Hello").Or.EqualTo("Goodbye"));
				//Console.WriteLine(i+")" + );
			}
		}

		[Test]
		public void ResolveWithNullGlobals()
		{
			string[] lines;
			ChatRuntime runtime;
			Command cmd;

			lines = new[] {
				"CHAT wine1 {noStart=true}",
				"SET a = $b",
				"SET b = c",
				"SAY $a"
			};
			runtime = new ChatRuntime(Tendar.AppConfig.Actors);
			runtime.ParseText(string.Join("\n", lines), false);
			//string content = "";
			//runtime.Chats().ForEach(c => { content += c.ToTree() + "\n\n"; });
			runtime.Chats().ForEach(c => c.Resolve(null));
			cmd = runtime.Chats().Last().commands.Last();
			Assert.That(cmd.Text(), Is.EqualTo("c"));

			lines = new[] {
				"CHAT wine1 {noStart=true}",
				"SET a = $b",
				"SET b = c",
				"SAY ${a}"
			};

			runtime = new ChatRuntime(Tendar.AppConfig.Actors);
			runtime.ParseText(string.Join("\n", lines), false);
			runtime.Chats().ForEach(c => c.Resolve(null));
			cmd = runtime.Chats().Last().commands.Last();
			Assert.That(cmd.Text(), Is.EqualTo("c"));
		}

		[Test]
		public void SetGrammarMode()
		{
			string[] lines = {
				"CHAT c1 {chatMode=grammar}",
				"$review=$greeting",
				"$greeting = (Hello | Goodbye)",
				"",
				"CHAT c2",
				"$review",
			};

			var chats = ChatParser.ParseText(String.Join("\n", lines), true);
			Assert.That(chats[0].commands[0].GetType(), Is.EqualTo(typeof(Set)));
			Assert.That(chats[1].commands[0].GetType(), Is.EqualTo(typeof(Say)));
			//chats[0].commands.ForEach(c => c.Resolve(globals));
		}

		[Test]
		public void SetOrEqualsGlobal()
		{
			string[] lines = {
				"CHAT c1 {type=a,stage=b}",
				"SET $review=$greeting",
				"SET $greeting = Hello | Goodbye",
				"SET $greeting |= See you later",
				"SAY $review",
			};
			var chat = ChatParser.ParseText(String.Join("\n", lines))[0];
			//Console.WriteLine(chat.ToTree());
			var last = chat.commands[chat.commands.Count - 1];
			Assert.That(last, Is.Not.Null);
			Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));
			chat.commands.ForEach(c => c.Resolve(globals));

			Assert.That(globals.ContainsKey("review"), Is.True);
			Assert.That(globals.ContainsKey("greeting"), Is.True);
			Assert.That(globals["greeting"],
						Is.EqualTo("(Hello | Goodbye | See you later)"));

			Say say = (Dialogic.Say)last;

			HashSet<String> unique = new HashSet<String>();
			for (int i = 0; i < 10; i++)
			{
				say.Resolve(globals);
				var said = say.Text();
				unique.Add(said);
				//Console.WriteLine(i + ") " + said);
				Assert.That(said, Is.EqualTo("Hello")
					.Or.EqualTo("Goodbye").Or.EqualTo("See you later"));
			}
			Assert.That(unique.Count, Is.EqualTo(3));
		}

		[Test]
		public void SetOrEqualsLocal()
		{
			string[] lines = {
				"CHAT c1 {type=a,stage=b}",
				"SET review=$greeting",
				"SET greeting = Hello | Goodbye",
				"SET greeting |= See you later",
				"SAY $review",
			};
			var chat = ChatParser.ParseText(String.Join("\n", lines))[0];
			//Console.WriteLine(chat.ToTree());
			var last = chat.commands[chat.commands.Count - 1];
			Assert.That(last, Is.Not.Null);
			Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));
			chat.commands.ForEach(c => c.Resolve(globals));

			Assert.That(chat.scope.ContainsKey("review"), Is.True);
			Assert.That(chat.scope.ContainsKey("greeting"), Is.True);
			Assert.That(chat.scope["greeting"],
						Is.EqualTo("(Hello | Goodbye | See you later)"));

			Say say = (Dialogic.Say)last;

			HashSet<String> unique = new HashSet<String>();
			for (int i = 0; i < 100; i++)
			{
				say.Resolve(globals);
				var said = say.Text();
				unique.Add(said);
				//Console.WriteLine(i+") "+said);
				Assert.That(said, Is.EqualTo("Hello")
					.Or.EqualTo("Goodbye").Or.EqualTo("See you later"));
			}
			Assert.That(unique.Count, Is.EqualTo(3));
		}


		[Test]
		public void SetPlusEquals()
		{
			string[] lines = {
				"CHAT c1 {type=a,stage=b}",
				"SET review=$greeting",
				"SET greeting = (Hello | Goodbye)",
				"SET greeting += Fred",
				"SAY $review",
			};
			var chat = ChatParser.ParseText(String.Join("\n", lines))[0];
			//Console.WriteLine(chat.ToTree());
			var last = chat.commands[chat.commands.Count - 1];
			Assert.That(last, Is.Not.Null);
			Assert.That(last.GetType(), Is.EqualTo(typeof(Say)));

			chat.commands.ForEach(c => c.Resolve(globals));

			//DumpGlobals();

			Assert.That(chat.scope.ContainsKey("review"), Is.True);
			Assert.That(chat.scope.ContainsKey("greeting"), Is.True);
			Assert.That(chat.scope["greeting"], Is.EqualTo("(Hello | Goodbye) Fred"));

			Say say = (Dialogic.Say)last;
			for (int i = 0; i < 10; i++)
			{
				say.Resolve(globals);
				var said = say.Text();
				//Console.WriteLine(i + ") " + said);
				Assert.That(said, Is.EqualTo("Hello Fred")
					.Or.EqualTo("Goodbye Fred"));
			}
		}

		/*[Test]
		public void WineReview()
		{
			string[] lines = {
				"CHAT wine1 {noStart=true,chatMode=grammar}",
				"SET review=$desc $fortune $ending",
				"ending =  $score | $end1",
				"ending |=  $end2",
				"desc =  Your expression is a \"(Plop|Fizz|Fail)\".",
				"desc +=  But, you don’t care do you? No, you don’t.",
				"fortune = How about a slap to reinstill your faith in humanity?",
				"score =  I'd have to rate this a 2. Try again with 'feeling'...",
				"end1 = There’s always time for a cold shower.",
				"end2 = Just give up please.",
				"SAY $review {speed=fast}"
			};

			var chat = ChatParser.ParseText(String.Join("\n", lines))[0];

			chat.commands.ForEach(c => c.Resolve(globals));
			//Console.WriteLine(chat.AsGrammar(globals), false);

			Say say = (Say)chat.commands.Last();//.Resolve()

			for (int i = 0; i < 10; i++)
			{
				var said = (string)say.Resolve(globals)[Meta.TEXT];
				//Console.WriteLine(i + ") " + said);
				Assert.That(said.StartsWith("Your expression is a", Util.IC), Is.True);
				Assert.That
					  (said.EndsWith("Try again with 'feeling'...", Util.IC)
					|| said.EndsWith("time for a cold shower.", Util.IC)
					|| said.EndsWith("Just give up please.", Util.IC), Is.True);
			}
		}*/

		[Test]
		public void FullGrammar()
		{
			string[] lines = {
				"CHAT full {noStart=true,chatMode=grammar}",
				"start =  $a $b $c",
				"a = A",// | A $a",
                "b = B",
				"c = D",
				"c |= E",
				"SAY $start"
			};

			var chat = ChatParser.ParseText(String.Join("\n", lines))[0];
			chat.commands.ForEach(c => c.Resolve(globals));
			Say say = (Say)chat.commands.Last();

			for (int i = 0; i < 10; i++)
			{
				var said = (string)say.Resolve(globals)[Meta.TEXT];
				//Console.WriteLine(i + ") " + said);
				Assert.That(said, Is.EqualTo("A B D").Or.EqualTo("A B E"));
			}
		}

		[Test]
		public void SimpleGrammarExpand()
		{
			string[] lines;
			string text;
			Chat chat;

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = C",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("A B C"));

			lines = new[]{
				"start =  $a $b",
				"a = A",
				"b = $c",
				"c = B $d",
				"d = C",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("A B C"));

			lines = new[]{
				"start =  $a $b $c",
				"a = A",
				"b = $c",
				"c = B $d",
				"d = C",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("A B C B C"));

			lines = new[]{
				"start =  $a ",
				"a = $b",
				"b = $c",
				"c = D",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("D"));

			lines = new[]{
				"start =  $a $b",
				"a = $c $d",
				"b = $d $c",
				"c = C",
				"d = D",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("C D D C"));
		}

		[Test]
		public void GrammarExpandWithOr()
		{
			string[] lines;
			string text;
			Chat chat;

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = C | D",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D)"));

			//Assert.That(text, Is.EqualTo("A B (C | D)"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = (C | D)",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D)"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = C",
				"c += D",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B C D"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = C",
				"c += | D",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D)"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = (C",
				"c += | D)",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D)"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = (C",
				"c += | D",
				"c += | E)",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D | E)"));

			lines = new[] {
				"start =  $a ($b $c)",
				"a = A",
				"b = B",
				"c = (C | D)",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A (B (C | D))"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = ($a | $b)",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (A | B)"));

			/* infinite loop
            lines = new[] {
                "start =  $a $b $c",
                "a = $b",
                "b = $c",
                "c = ($a | $b | D)",
            };
            text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Realize(globals);
            Console.WriteLine(chat.Expand(globals, "$start"));
            Assert.That(chat.ExpandNoGroups(globals, "$start"), Is.EqualTo("A B (A | B)")); */
		}


		internal static string BindSymbolsOnly(Chat c, string start)
		{
			return new Resolver(c.runtime).BindSymbols(start, c, globals);
		}

		[Test]
		public void GrammarExpandWithScopedOrEquals()
		{
			string[] lines;
			string text;
			Chat chat;

			lines = new[] {
				"$start =  $a $b $c",
				"$a = A",
				"$b = B",
				"$c = C",
				"$c |= $fish.name",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			//Console.WriteLine(globals.Stringify());
			//Console.WriteLine(chat.scope.Stringify());
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | Fred)"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = C",
				"c |= $fish.name",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			//Console.WriteLine(chat);
			//Console.WriteLine(globals.Stringify());
			//Console.WriteLine(chat.scope.Stringify());
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | Fred)"));
		}

		[Test]
		public void GrammarExpandWithOrEqualsGlobal()
		{
			string[] lines;
			string text;
			Chat chat;

			lines = new[] {
				"$start =  $a $b $c",
				"$a = A",
				"$b = B",
				"$c = C | D",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D)"));

			lines = new[] {
				"$start =  $a $b $c",
				"$a = A",
				"$b = B",
				"$c = C",
				"$c |= D",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D)"));

			lines = new[] {
				"$start =  $a $b $c",
				"$a = A",
				"$b = B",
				"$c = C | D",
				"$c |= E",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D | E)"));

			lines = new[] {
				"$start =  $a $b $c",
				"$a = A",
				"$b = B",
				"$c = (C | D)",
				"$c |= E",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D | E)"));

			lines = new[] {
				"$start =  $a $b $c",
				"$a = A",
				"$b = B",
				"$c = C (C | D)",
				"$c |= E",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C (C | D) | E)"));

			lines = new[] {
				"$start = $a $b $c",
				"$a = A",
				"$b = B",
				"$c = D | E",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (D | E)"));

			lines = new[] {
				"$start = $a $b $c",
				"$a = A",
				"$b = B",
				"$c = $d | $e",
				"$d = D",
				"$e = E",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (D | E)"));

			lines = new[] {
				"$start = $desc $fortune $ending",
				"$ending = $score | $end-phrase",
				"$desc = A",
				"$fortune = B",
				"$score = C",
				"$end-phrase = D"
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D)"));
		}

		[Test]
		public void GrammarExpandWithPlusEquals()
		{
			string[] lines;
			string text;
			Chat chat;

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = C | D",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);

			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D)"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = C",
				"c += D",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B C D"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = (C | D)",
				"c += E",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B ((C | D) E)"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = C (C | D)",
				"c += E",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C (C | D) E)"));
		}

		[Test]
		public void GrammarExpandWithOrEquals()
		{
			string[] lines;
			string text;
			Chat chat;

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = C | D",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);

			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D)"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = C",
				"c |= D",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D)"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = (C | D)",
				"c |= E",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D | E)"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = C (C | D)",
				"c |= E",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C (C | D) | E)"));
		}

		[Test]
		public void GrammarExpandOrs()
		{
			string[] lines;
			string text;
			Chat chat;

			lines = new[] {
				"start = $a $b $c",
				"a = A",
				"b = B",
				"c = D | E",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (D | E)"));

			lines = new[] {
				"start = $a $b $c",
				"a = A",
				"b = B",
				"c = $d | $e",
				"d = D",
				"e = E",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (D | E)"));

			lines = new[] {
				"start = $desc $fortune $ending",
				"ending = $score | $end-phrase",
				"desc = A",
				"fortune = B",
				"score = C",
				"end-phrase = D"
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(BindSymbolsOnly(chat, "$start"), Is.EqualTo("A B (C | D)"));
		}

		[Test]
		public void GrammarExpandWithOrEqualsDoGroups()
		{
			string[] lines;
			string text;
			Chat chat;

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = C",
				"c |= D",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("A B C").Or.EqualTo("A B D"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = C | D",
				"c |= E",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("A B C").Or.EqualTo("A B D").Or.EqualTo("A B E"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = (C | D)",
				"c |= E",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("A B C").Or.EqualTo("A B D").Or.EqualTo("A B E"));

			lines = new[] {
				"start =  $a $b $c",
				"a = A",
				"b = B",
				"c = C (C | D)",
				"c |= E",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat._Expand(globals, "$start"), Is.EqualTo("A B E").Or.EqualTo("A B C C").Or.EqualTo("A B C D"));

			lines = new[] {
				"start =  $a $b",
				"a = The hungry dog",
				"a |= The angry cat",
				"b = bit the tiny child",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat._Expand(globals, "$start"),
						Is.EqualTo("The hungry dog bit the tiny child")
						.Or.EqualTo("The angry cat bit the tiny child"));

			lines = new[] {
				"start =  $a $b",
				"a = The hungry dog",
				"a |= The angry cat",
				"b = bit the tiny child",
			};
			text = "CHAT X {chatMode=grammar}\n" + String.Join("\n", lines);
			chat = (Chat)ChatParser.ParseText(text, true)[0]; chat.Resolve(globals);
			Assert.That(chat._Expand(globals, "$start"),
				Is.EqualTo("The hungry dog bit the tiny child")
				.Or.EqualTo("The angry cat bit the tiny child"));
		}

		public static string _AllCaps(string s) => s.ToUpper();
	}
}
