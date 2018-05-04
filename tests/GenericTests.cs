using System.Collections.Generic;
using NUnit.Framework;

namespace Dialogic
{
    public class GenericTests
    {
        public const bool NO_VALIDATORS = true;

        public static IDictionary<string, object> globals;

        [SetUp]
        public void Init()
        {
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
                { "fish",  new Fish("Fred")}
            };
        }

        //public class Fish
        //{
        //    public string name { get; protected set; }
        //    public Flipper flipper { get; protected set; }

        //    public Fish(string name)
        //    {
        //        this.name = name;
        //        this.flipper = new Flipper(1.1);
        //    }
        //}

        //public class Flipper
        //{
        //    public double speed { get; protected set; }
        //    public Flipper(double s)
        //    {
        //        this.speed = s;
        //    }
        //}


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
}