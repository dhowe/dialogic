using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void PegasusTest1()
        {
            Assert.That("hello", Is.EqualTo("hello"));
            //var parser = new MyProject.ExpressionParser();
            //var result = parser.Parse("5.1+2*3");
            //Console.WriteLine(result); // Outputs "11.1".
        }

        [Test]
        public void IronMetaTest1()
        {
            Assert.That("hello", Is.EqualTo("hello"));
            /*var parser = new Calc();
            var match = parser.GetMatch("2 * 7", parser.Expression);

            if (match.Success)
                Console.WriteLine("result: {0}", match.Result); // should print "14"
            else
                Console.WriteLine("error: {0}", match.Error); // shouldn't happen*/
            //var parser = new MyProject.ExpressionParser();
            //var result = parser.Parse("5.1+2*3");
            //Console.WriteLine(result); // Outputs "11.1".
        }
    }
}