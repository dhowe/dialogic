﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Dialogic
{
    [TestFixture]
    class BindingTests : GenericTests
    {
        [Test]
        public void PreloadingTests()
        {
            string[] lines = new[] {
                "CHAT c1",
                "SET ab = hello",
                "SAY $ab $de",

                "CHAT c2 {preload=true}",
                "SET $de = preload",
            };
            ChatRuntime rt = new ChatRuntime();
            rt.ParseText(String.Join("\n", lines), true);
            rt.Preload(globals);

            //rt["c1"].Resolve(globals);
            var s = rt.InvokeImmediate(globals);
            Assert.That(s, Is.EqualTo("hello preload"));
            //chat = (Chat) .Resolve(globals);
            //Assert.That(chat.commands[1].Text(), Is.EqualTo("hello"));
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
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Resolve(globals);
            Assert.That(chat.commands[1].Text(), Is.EqualTo("hello"));

            // global-miss
            lines = new[] {
                "CHAT c1",
                "SET ab = hello",
                "SAY $ab",
            };
            text = String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Resolve(globals);
            Assert.That(chat.commands[1].Text(), Is.EqualTo("hello"));

            // global-hit
            lines = new[] {
                "CHAT c1",
                "SAY $animal",
            };
            text = String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Resolve(globals);
            Assert.That(chat.commands[0].Text(), Is.EqualTo("dog"));

            // global-properties
            lines = new[] {
                "CHAT c1",
                "SAY $fish.name",
            };
            text = String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Resolve(globals);
            Assert.That(chat.commands[0].Text(), Is.EqualTo("Fred"));

            // global-bounded
            lines = new[] {
                "CHAT c1",
                "SAY ${fish.name}",
            };
            text = String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Resolve(globals);
            Assert.That(chat.commands[0].Text(), Is.EqualTo("Fred"));

            // global-nested
            lines = new[] {
                "CHAT c1",
                "SAY $fish.flipper.speed",
            };
            text = String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Resolve(globals);
            Assert.That(chat.commands[0].Text(), Is.EqualTo("1.1"));

            // global-nested-bounded
            lines = new[] {
                "CHAT c1",
                "SAY ${fish.flipper.speed}",
            };
            text = String.Join("\n", lines);
            chat = (Chat)ChatParser.ParseText(text, true)[0];chat.Resolve(globals);
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
    }
}