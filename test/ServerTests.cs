using System;
using System.Collections.Generic;

using Dialogic.Server;
using NUnit.Framework;
using Flurl.Http;

namespace Dialogic.Test
{
    // NEXT: selectionStart/End, File/URL loading
    // TODO: disable tests automatically on travis

    [TestFixture]
    public class ServerTests : GenericTests
    {
        const string ServerUrl = "http://localhost:8082/dialogic/server/";

        // Set to true to test the running editor via http (always false in travis)
        public static bool DO_HTTP_TESTS = true && Environment.GetEnvironmentVariable("CI") != "true";

        // ------------------------------- HTTP Tests --------------------------------

        [Test]
        public void ValidateHttp()
        {
            if (!DO_HTTP_TESTS) return;

            var expect = "CHAT Hello\nSAY Hi";
            var task = ServerUrl
                .PostUrlEncodedAsync(new { type = "validate", code = expect })
                .ReceiveJson<Result>();
            task.Wait();

            Console.WriteLine(task);

            Assert.AreEqual(Result.OK, task.Result.Status);
            Assert.AreEqual(expect, task.Result.Data);

            task = ServerUrl
                .PostUrlEncodedAsync(new { type = "validate", code = expect, useValidators = "false" })
                .ReceiveJson<Result>();
            task.Wait();

            Assert.AreEqual(Result.OK, task.Result.Status);
            //Console.WriteLine(task.Result.Data);
            Assert.AreEqual(expect, task.Result.Data);

            expect = string.Join("\n", new[] {
                "CHAT RePrompt",
                "DO #SadSpin",
                "ASK(Really| Awww), don't you want to play a game?",
                "OPT sure #Game",
                "OPT $neg #RePrompt"});
            task = ServerUrl
                .PostUrlEncodedAsync(new { type = "validate", code = expect, useValidators = "false" })
                .ReceiveJson<Result>();
            task.Wait();

            Console.WriteLine(task.Result.Data);
            Assert.AreEqual(Result.OK, task.Result.Status);
            Assert.AreEqual(expect, task.Result.Data);
        }

        [Test]
        public void ValidateErrorHttp()
        {
            if (!DO_HTTP_TESTS) return;

            var expect = "{ \"status\": \"ERROR\", \"lineNo\": 1, \"data\": \"Line 1 :: CHAT Hello\n\nValidator: missing required meta-key 'type', 'noStart' or 'preload'\" }";

            var task = ServerUrl
                .PostUrlEncodedAsync(new { type = "validate", code = "CHAT Hello\nSAY Hi", useValidators = "true" })
                .ReceiveJson<Result>();
            task.Wait();
            Assert.AreEqual(Result.ERR, task.Result.Status);
            //Console.WriteLine(task.Result.Data);
            Assert.AreEqual(1, task.Result.LineNo);
            Assert.AreEqual(expect, task.Result.ToJSON());
        }

        [Test]
        public void ExecuteHttp()
        {
            if (!DO_HTTP_TESTS) return;

            var expect = "Hi";
            var task = ServerUrl
                .PostUrlEncodedAsync(new { type = "execute", code = "CHAT Hello\nSAY Hi" })
                .ReceiveJson<Result>();
            task.Wait();
            //Console.WriteLine(task.Result.Data);
            Assert.AreEqual(Result.OK, task.Result.Status);
            Assert.AreEqual(expect, task.Result.Data);
        }

        [Test]
        public void VisualizeHttp1()
        {
            if (!DO_HTTP_TESTS) return;

            var expect = "var chats = {\n  \"1\": \"CHAT Hello\nSAY Hi\",\n};\nvar nodes = new vis.DataSet([\n  { id: 1, label: 'Hello' },\n]);\nvar edges = new vis.DataSet([\n]);";

            var task = ServerUrl
                .PostUrlEncodedAsync(new { type = "visualize", code = "CHAT Hello\nSAY Hi" })
                .ReceiveJson<Result>();
            task.Wait();
            Assert.AreEqual(Result.OK, task.Result.Status);
            Assert.AreEqual(expect, task.Result.Data);

        }

        [Test]
        public void VisualizeHttp2()
        {
            if (!DO_HTTP_TESTS) return;

            var expect = "var chats = {\n  \"1\": \"CHAT A\nSAY a\nGO #b\",\n  \"2\": \"CHAT B\nSAY b\",\n};\nvar nodes = new vis.DataSet([\n  { id: 1, label: 'A' },\n  { id: 2, label: 'B' },\n]);\nvar edges = new vis.DataSet([\n  { from: 1, to: -1 },\n]);";

            var task = ServerUrl
                .PostUrlEncodedAsync(new { type = "visualize", code = "CHAT A\nSAY a\nGO #b\n\nCHAT B\nSAY b" })
                .ReceiveJson<Result>();
            task.Wait();
            Assert.AreEqual(Result.OK, task.Result.Status);
            Assert.AreEqual(expect, task.Result.Data);
        }

        // ------------------------------- Direct Tests --------------------------------

        [Test]
        public void ErrorTests()
        {
            //Assert.Throws<Exception>(() => Operator.SW.Invoke(null, "hello"));

            var lines = "CHAT Hello\nSAY Hi";
            var expect = "{ \"status\": \"ERROR\", \"lineNo\": 1, \"data\": \"Line 1 :: CHAT Hello\n\nValidator: missing required meta-key 'type', 'noStart' or 'preload'\" }";
            var kvs = new Dictionary<string, string>() {
                { "type", "validate" }, { "code", lines }, { "useValidators", "true" }};
            var result = RequestHandler.Validate(kvs);
            // Console.WriteLine(result);
            Assert.That(result, Is.EqualTo(expect));
        }

        [Test]
        public void ValidateDirect()
        {

            var lines = "CHAT Hello\nSAY Hi";
            var kvs = new Dictionary<string, string>() {
                { "type", "validate" }, { "code", lines }, { "useValidators", "false" }};
            var result = RequestHandler.Validate(kvs);
            Assert.That(result, Is.EqualTo(JSONResult(lines)));

            lines = "CHAT Hello {noStart=true}\nSAY Hi";
            kvs = new Dictionary<string, string>() {
                { "type", "validate" }, { "code", lines }, { "useValidators", "true" }};
            result = RequestHandler.Validate(kvs);
            Assert.That(result, Is.EqualTo(JSONResult(lines)));
        }

        [Test]
        public void ValidateDirect2()
        {

            var text = string.Join("\n", new [] {
                "CHAT RePrompt",
                "DO #SadSpin",
                "ASK(Really| Awww), don't you want to play a game?",
                "OPT sure #Game",
                "OPT $neg #RePrompt"});
            var kvs = new Dictionary<string, string>() {
                { "type", "validate" }, { "code", string.Join("\n", text) }, { "useValidators", "false" }};
            var result = RequestHandler.Validate(kvs);
            Assert.That(result, Is.EqualTo(JSONResult(text)));
        }

        [Test]
        public void ExecuteDirect()
        {
            var kvs = new Dictionary<string, string>() {
                { "type", "execute" }, { "code", "CHAT Hello\nSAY Hi" }};
            Assert.That(RequestHandler.Execute(kvs), Is.EqualTo(JSONResult("Hi")));
        }

        string JSONResult(string data) =>
            "{ \"status\": \"OK\", \"lineNo\": -1, \"data\": \"" + data + "\" }";
    }
}
