using System;
using System.Collections.Generic;
using Client;
using NUnit.Framework;
using Parser;
using System.Linq;
using Superpower.Model;

namespace Dialogic.Test
{

    [TestFixture]
    public class TokenizerTests : GenericTests
    {
        //[Test]
        public void FailingTests()
        {
            var result = DiaTokenizer.Instance.TryTokenize("SAY Hello:");
            Out(result);
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(2));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.SAY));
            Assert.That(result.Value.ElementAt(1).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(1).Span.ToString(), Is.EqualTo("Hello:"));
        }

        [Test]
        public void InvalidStrings()
        {
            Result<TokenList<DiaToken>> result;

            result = DiaTokenizer.Instance.TryTokenize("$2badvar1");
            Assert.That(result.HasValue, Is.False);

            result = DiaTokenizer.Instance.TryTokenize("#2badhash2");
            Assert.That(result.HasValue, Is.False);

            result = DiaTokenizer.Instance.TryTokenize("#=badhash3");
            Assert.That(result.HasValue, Is.False);

            result = DiaTokenizer.Instance.TryTokenize("$,badvar4");
            Assert.That(result.HasValue, Is.False);
        }

        [Test]
        public void TokenizeActor()
        {
            Result<TokenList<DiaToken>> result;

            result = DiaTokenizer.Instance.TryTokenize("Dave:");
            //Out(result);
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(1));

            result = DiaTokenizer.Instance.TryTokenize("Dave: x");
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(2));
        }

        [Test]
        public void TokenizeMetaData()
        {
            Result<TokenList<DiaToken>> result;

            // Fails
            result = DiaTokenizer.Instance.TryTokenize("SAY Hello, you { key=val,key2=val2 } ");
            //Out(result);
            Assert.That(result.HasValue, Is.True);
            //Assert.That(result.Value.Count(), Is.EqualTo(11));
            //Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.SAY));
            //Assert.That(result.Value.ElementAt(1).Kind, Is.EqualTo(DiaToken.String));
            //Assert.That(result.Value.ElementAt(1).Span.ToString(), Is.EqualTo("Hello, you "));
            //Assert.That(result.Value.ElementAt(2).Kind, Is.EqualTo(DiaToken.LBrace));
            //Assert.That(result.Value.ElementAt(3).Kind, Is.EqualTo(DiaToken.String));
            //Assert.That(result.Value.ElementAt(3).Span.ToString(), Is.EqualTo("key"));
            //Assert.That(result.Value.ElementAt(4).Kind, Is.EqualTo(DiaToken.Equal));
            //Assert.That(result.Value.ElementAt(5).Kind, Is.EqualTo(DiaToken.String));
            //Assert.That(result.Value.ElementAt(5).Span.ToString(), Is.EqualTo("val"));
            //Assert.That(result.Value.ElementAt(6).Kind, Is.EqualTo(DiaToken.Comma));
            //Assert.That(result.Value.ElementAt(7).Kind, Is.EqualTo(DiaToken.String));
            //Assert.That(result.Value.ElementAt(7).Span.ToString(), Is.EqualTo("key2"));
            //Assert.That(result.Value.ElementAt(8).Kind, Is.EqualTo(DiaToken.Equal));
            //Assert.That(result.Value.ElementAt(9).Kind, Is.EqualTo(DiaToken.String));
            //Assert.That(result.Value.ElementAt(9).Span.ToString(), Is.EqualTo("val2 "));
            //Assert.That(result.Value.ElementAt(10).Kind, Is.EqualTo(DiaToken.RBrace));

            Assert.That(result.Value.Count(), Is.EqualTo(13));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.SAY));
            Assert.That(result.Value.ElementAt(1).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(1).Span.ToString(), Is.EqualTo("Hello"));
            Assert.That(result.Value.ElementAt(2).Kind, Is.EqualTo(DiaToken.Comma));
            Assert.That(result.Value.ElementAt(3).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(3).Span.ToString(), Is.EqualTo("you "));
            Assert.That(result.Value.ElementAt(4).Kind, Is.EqualTo(DiaToken.LBrace));
            Assert.That(result.Value.ElementAt(5).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(5).Span.ToString(), Is.EqualTo("key"));
            Assert.That(result.Value.ElementAt(6).Kind, Is.EqualTo(DiaToken.Equal));
            Assert.That(result.Value.ElementAt(7).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(7).Span.ToString(), Is.EqualTo("val"));
            Assert.That(result.Value.ElementAt(8).Kind, Is.EqualTo(DiaToken.Comma));
            Assert.That(result.Value.ElementAt(9).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(9).Span.ToString(), Is.EqualTo("key2"));
            Assert.That(result.Value.ElementAt(10).Kind, Is.EqualTo(DiaToken.Equal));
            Assert.That(result.Value.ElementAt(11).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(11).Span.ToString(), Is.EqualTo("val2 "));
            Assert.That(result.Value.ElementAt(12).Kind, Is.EqualTo(DiaToken.RBrace));


            //return;
            result = DiaTokenizer.Instance.TryTokenize("SAY Hello you { key=val } ");

            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(7));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.SAY));
            Assert.That(result.Value.ElementAt(1).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(1).Span.ToString(), Is.EqualTo("Hello you "));
            Assert.That(result.Value.ElementAt(2).Kind, Is.EqualTo(DiaToken.LBrace));
            Assert.That(result.Value.ElementAt(3).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(3).Span.ToString(), Is.EqualTo("key"));
            Assert.That(result.Value.ElementAt(4).Kind, Is.EqualTo(DiaToken.Equal));
            Assert.That(result.Value.ElementAt(5).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(5).Span.ToString(), Is.EqualTo("val "));
            Assert.That(result.Value.ElementAt(6).Kind, Is.EqualTo(DiaToken.RBrace));


            result = DiaTokenizer.Instance.TryTokenize("SAY Hello { key=val,key2=val2 } ");
            //Out(result);

            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(11));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.SAY));
            Assert.That(result.Value.ElementAt(1).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(1).Span.ToString(), Is.EqualTo("Hello "));
            Assert.That(result.Value.ElementAt(2).Kind, Is.EqualTo(DiaToken.LBrace));
            Assert.That(result.Value.ElementAt(3).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(3).Span.ToString(), Is.EqualTo("key"));
            Assert.That(result.Value.ElementAt(4).Kind, Is.EqualTo(DiaToken.Equal));
            Assert.That(result.Value.ElementAt(5).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(5).Span.ToString(), Is.EqualTo("val"));
            Assert.That(result.Value.ElementAt(6).Kind, Is.EqualTo(DiaToken.Comma));
            Assert.That(result.Value.ElementAt(7).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(7).Span.ToString(), Is.EqualTo("key2"));
            Assert.That(result.Value.ElementAt(8).Kind, Is.EqualTo(DiaToken.Equal));
            Assert.That(result.Value.ElementAt(9).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(9).Span.ToString(), Is.EqualTo("val2 "));
            Assert.That(result.Value.ElementAt(10).Kind, Is.EqualTo(DiaToken.RBrace));


            result = DiaTokenizer.Instance.TryTokenize("SAY Hello { at=$boy } ");
            Assert.That(result.HasValue, Is.True);
            //Out(result);
            Assert.That(result.Value.Count(), Is.EqualTo(7));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.SAY));
            Assert.That(result.Value.ElementAt(1).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(2).Kind, Is.EqualTo(DiaToken.LBrace));
            Assert.That(result.Value.ElementAt(3).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(3).Span.ToString(), Is.EqualTo("at"));
            Assert.That(result.Value.ElementAt(4).Kind, Is.EqualTo(DiaToken.Equal));
            Assert.That(result.Value.ElementAt(5).Kind, Is.EqualTo(DiaToken.Symbol));
            Assert.That(result.Value.ElementAt(5).Span.ToString(), Is.EqualTo("$boy"));
            Assert.That(result.Value.ElementAt(6).Kind, Is.EqualTo(DiaToken.RBrace));


            result = DiaTokenizer.Instance.TryTokenize("SAY $a.do()");
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(5));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.SAY));
            Assert.That(result.Value.ElementAt(1).Kind, Is.EqualTo(DiaToken.Symbol));
            Assert.That(result.Value.ElementAt(1).Span.ToString(), Is.EqualTo("$a"));
            Assert.That(result.Value.ElementAt(2).Kind, Is.EqualTo(DiaToken.Dot));
            Assert.That(result.Value.ElementAt(3).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(3).Span.ToString(), Is.EqualTo("do"));
            Assert.That(result.Value.ElementAt(4).Kind, Is.EqualTo(DiaToken.ParenPair));
        }

        [Test]
        public void TokenizeSimpleSymbols()
        {
            Result<TokenList<DiaToken>> result;

            result = DiaTokenizer.Instance.TryTokenize("CHAT #chatLabel");
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(2));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.CHAT));
            Assert.That(result.Value.ElementAt(1).Kind, Is.EqualTo(DiaToken.Label));
            Assert.That(DiaTokenizer.Instance.TryTokenize("CHAT $chat2Label").HasValue, Is.True);
            Assert.That(DiaTokenizer.Instance.TryTokenize("CHAT $chat_Label").HasValue, Is.True);
            Assert.That(DiaTokenizer.Instance.TryTokenize("CHAT $_chat2Label2").HasValue, Is.True);

            result = DiaTokenizer.Instance.TryTokenize("SAY $chatLabel");
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(2));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.SAY));
            Assert.That(result.Value.ElementAt(1).Kind, Is.EqualTo(DiaToken.Symbol));
            Assert.That(result.Value.ElementAt(1).Span.ToString(), Is.EqualTo("$chatLabel"));

            result = DiaTokenizer.Instance.TryTokenize("SAY $$chatLabel");
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(2));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.SAY));
            Assert.That(result.Value.ElementAt(1).Kind, Is.EqualTo(DiaToken.Symbol));
            Assert.That(result.Value.ElementAt(1).Span.ToString(), Is.EqualTo("$$chatLabel"));


            Assert.That(DiaTokenizer.Instance.TryTokenize("SAY $chat2Label").HasValue, Is.True);
            Assert.That(DiaTokenizer.Instance.TryTokenize("SAY $chat_Label").HasValue, Is.True);
            Assert.That(DiaTokenizer.Instance.TryTokenize("SAY $_chat2Label2").HasValue, Is.True);
        }


        [Test]
        public void TokenizeSimpleCommands()
        {
            Result<TokenList<DiaToken>> result;

            result = DiaTokenizer.Instance.TryTokenize("SAY Hello,");
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(3));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.SAY));
            Assert.That(result.Value.ElementAt(1).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(1).Span.ToString(), Is.EqualTo("Hello"));
            Assert.That(result.Value.ElementAt(2).Kind, Is.EqualTo(DiaToken.Comma));

            result = DiaTokenizer.Instance.TryTokenize("SAY");
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(1));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.SAY));

            result = DiaTokenizer.Instance.TryTokenize("SAY ");
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(1));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.SAY));

            result = DiaTokenizer.Instance.TryTokenize("SAY Hello");
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(2));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.SAY));

            result = DiaTokenizer.Instance.TryTokenize("SAY Hello you");
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(2));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.SAY));
            Assert.That(result.Value.ElementAt(1).Kind, Is.EqualTo(DiaToken.Text));
            Assert.That(result.Value.ElementAt(1).Span.ToString(), Is.EqualTo("Hello you"));


            result = DiaTokenizer.Instance.TryTokenize("Hello:SAY");
            Assert.That(result.HasValue, Is.True);
            Assert.That(result.Value.Count(), Is.EqualTo(2));
            Assert.That(result.Value.ElementAt(0).Kind, Is.EqualTo(DiaToken.Actor));
            Assert.That(result.Value.ElementAt(0).Span.ToString(), Is.EqualTo("Hello:"));
            Assert.That(result.Value.ElementAt(1).Kind, Is.EqualTo(DiaToken.SAY));
        }
    }
}