using NUnit.Framework;
using Dialogic;
using System;
using System.Collections.Generic;
using System.IO;

namespace dialogic
{
    [TestFixture]
    public class SubstTests
    {
        Dictionary<string, object> globals = new Dictionary<string, object>() {
                { "animal", "dog" },
                { "prep", "then" },
                { "group", "(a|b)" },
                { "cmplx", "($group | $prep)" },
                { "count", 4 }
        };

        [Test]
        public void TestReplaceMeta()
        {
            var s = @"SAY The $animal yawned {animal=$animal}";
            Command c = ChatParser.ParseText(s)[0].commands[0];
            Assert.That(c.GetType(), Is.EqualTo(typeof(Say)));
            Substitutions.DoMeta(c.Meta(), globals);
            Assert.That(c.GetMeta("animal"), Is.EqualTo("dog"));
        }

        [Test]
        public void TestReplaceVars()
        {
            var s = @"SAY The $animal woke $count times";
            Substitutions.DoVars(ref s, globals);
            //Console.WriteLine("Running SubstitutionTests.Test2 :: " + s);
            Assert.That(s, Is.EqualTo("SAY The dog woke 4 times"));
        }

        [Test]
        public void TestReplaceGroups()
        {
            var txt = "The boy was (sad | happy)";
            string[] ok = { "The boy was sad", "The boy was happy" };
            //Console.WriteLine("Running SubstitutionTests.Test2 :: " + s);
            for (int i = 0; i < 10; i++)
            {
                string s = txt;
                Substitutions.DoGroups(ref s);
                //Console.WriteLine(i + ") " + s);
                CollectionAssert.Contains(ok, s);
            }

            txt = "The boy was (sad | happy | dead)";
            ok = new string[] { "The boy was sad", "The boy was happy", "The boy was dead" };
            //Console.WriteLine("Running SubstitutionTests.Test2 :: " + s);
            for (int i = 0; i < 10; i++)
            {
                string s = txt;
                Substitutions.DoGroups(ref s);
                //Console.WriteLine(i + ") " + s);
                CollectionAssert.Contains(ok, s);
            }
        }

        [Test]
        public void TestReplace1()
        {
            var s = @"SAY The $animal woke and $prep (ate|ate)";
            Substitutions.Do(ref s, globals);
            Assert.That(s, Is.EqualTo("SAY The dog woke and then ate"));
        }

        [Test]
        public void TestReplace2()
        {
            var txt = "letter $group";
            string[] ok = { "letter a", "letter b" };
            for (int i = 0; i < 10; i++)
            {
                var s = txt;
                Substitutions.Do(ref s, globals);
                //Console.WriteLine(i + ") " + s);
                CollectionAssert.Contains(ok, s);
            }
        }

        [Test]
        public void TestReplace3()
        {
            var txt = "letter $cmplx";
            string[] ok = { "letter a", "letter b", "letter then" };
            string[] res = new string[10]; 
            for (int i = 0; i < res.Length; i++)
            {
                var s = txt;
                Substitutions.Do(ref s, globals);
                //Console.WriteLine(i + ") " + s);
                res[i] = s;
            }
            for (int i = 0; i < res.Length; i++)
            {
                CollectionAssert.Contains(ok, res[i]);
            }
        }

    }
}
