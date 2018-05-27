using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Dialogic
{
    //[TestFixture]
    class KnownIssues : GenericTests
    {
        //[Test]
        public void SimpleTransformResolution()
        {
            string result, text;
            TxForm tran;
            Chat c1 = null;

            text = "(a (then)).Cap()";
            var txs = TxForm.Parse(text, c1);
            Assert.That(txs.Count, Is.EqualTo(1));
            tran = txs[0];
            result = tran.Resolve();
            Assert.That(result.ToString(), Is.EqualTo("A then"));
            result = tran.Replace(text, result);
            Assert.That(result.ToString(), Is.EqualTo("A then)"));
        }

        //[Test]
        public void PartialTransformIssue()
        {
            ChatRuntime rt = new ChatRuntime();
            Resolver.DBUG = true;
            rt.ParseText("SET $test = (a) (b)\nSAY $test.Cap()", true);
            Assert.That(rt.InvokeImmediate(globals), Is.EqualTo("A b"));

            rt = new ChatRuntime();
            //Resolver.DBUG = true;
            rt.ParseText("SET $test = (a | a) (b | b)\nSAY $test.Cap()", true);
            Assert.That(rt.InvokeImmediate(globals), Is.EqualTo("A b"));
        }

        //[Test]
        public void TransformIssue()
        {
            string res;

            res = new Resolver(null).Bind("(ab).Cap()", CreateParentChat("c"), null);
            Console.WriteLine("1: " + res);
            Assert.That(res, Is.EqualTo("Ab"));

            res = new Resolver(null).Bind("(a b).Cap()", CreateParentChat("c"), null);
            Console.WriteLine("2: " + res);
            Assert.That(res, Is.EqualTo("A b"));


            Resolver.DBUG = true;
            res = new Resolver(null).Bind("((a b)).Cap()", CreateParentChat("c"), null);
            Console.WriteLine("3: " + res);
            Assert.That(res, Is.EqualTo("A b"));

            res = new Resolver(null).Bind("((a) (b)).Cap()", CreateParentChat("c"), null);
            Console.WriteLine("4: " + res);
            Assert.That(res, Is.EqualTo("A b"));

            res = new Resolver(null).Bind("((a)(b)).Cap()", CreateParentChat("c"), null);
            Console.WriteLine("5: " + res);
            Assert.That(res, Is.EqualTo("Ab"));
        }
    }
}