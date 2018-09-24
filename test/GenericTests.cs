using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Dialogic.Test
{
    public class GenericTests
    {
        protected const IAppConfig NO_VALIDATORS = null;

		protected static IDictionary<string, object> globals;

		protected static Chat CreateParentChat(string name)
        {
            // create a realized Chat with the full set of global props
            var c = Chat.Create(name);
            foreach (var prop in globals.Keys) c.SetMeta(prop, globals[prop]);
            c.Resolve(globals);
            return c;
        }
        
        [SetUp]
        public void Init()
        {
			Resolver.DBUG = false;

            globals = new Dictionary<string, object>
            {
                { "obj-prop", "dog" },
                { "animal", "dog" },
                { "prep", "then" },
                { "group", "(a|b)" },
                { "cmplx", "($group | $prep)" },
                { "count", 4 },
                { "prop1", "hum" },
                { "name", "Jon" },
                { "verb", "melt" },
                { "a", "A" },
                { "recur", "a" },
                { "fish",  new Fish("Fred")}
            };
        }

        public class Fish
        {
            public static string species { get; protected set; }
            public static string GetSpecies() { return species; }

            public string name { get; protected set; }
            public Flipper flipper { get; protected set; }

            public Flipper GetFlipper() { return flipper; }

            public double GetFlipperSpeed() { return flipper.speed; }

            private int id = 9;

            public int Id() { return id; }
            public void Id(int id) { this.id = id; }

            public Fish(string name)
            {
                species = "Oscar";
                this.name = name;
                this.flipper = new Flipper(1.1);
            }

            public override string ToString()
            {
                return species + ": " + name;
            }
        }

        public class Flipper
        {
            public double speed { get; protected set; }
            public Flipper(double s)
            {
                this.speed = s;
            }
            public override string ToString()
            {
                return this.speed.ToString();
            }
        }      
    }

	public static class TestExtensions
    {
		public static bool IsOneOf<T>(this T candidate, IEnumerable<T> expected)
        {
            if (expected.Contains(candidate)) return true;         
            var msg = string.Format("Expected one of: [{0}]. Actual: '{1}'", ", "
                .InsertBetween(expected.Select(x => "'"+Convert.ToString(x)+"'")), candidate);
            Assert.Fail(msg);
            return false;
        }

        public static bool IsOneOf(this string candidate, params string[] expected)
        {
            if (expected.Contains(candidate)) return true;
            var msg = string.Format("Expected one of: [{0}]. Actual: '{1}'", ", "
                .InsertBetween(expected.Select(x => "'" + Convert.ToString(x) + "'")), candidate);
            Assert.Fail(msg);
            return false;
        }

        private static string InsertBetween(this string delimiter, IEnumerable<string> items)
        {
            var builder = new StringBuilder();
            foreach (var item in items)
            {
                if (builder.Length != 0) builder.Append(delimiter);
                builder.Append(item);
            }
            return builder.ToString();
        }      
    }
}