using System;
using System.Collections.Generic;
using Dialogic.NewServer;
using NUnit.Framework;
using Flurl.Http;

namespace Dialogic.Test
{

    [TestFixture]
    public class ServerTests : GenericTests
    {
        const string ServerUrl = "http://localhost:8082/dialogic/editor/";

        public static bool DO_HTTP_TESTS = false;

        // ------------------------------- HTTP Tests --------------------------------

        [Test]
        public void ValidateHttp()
        {
            if (!DO_HTTP_TESTS) return;

            var expect = "CHAT Hello\nSAY Hi";
            var task = ServerUrl
                .PostUrlEncodedAsync(new { type = "validate", code = "CHAT Hello\nSAY Hi" })
                .ReceiveJson<Result>();
            task.Wait();
            Assert.AreEqual(Result.OK, task.Result.Status);
            //Console.WriteLine(task.Result.Data);
            Assert.AreEqual(expect, task.Result.Data);


            task = ServerUrl
                .PostUrlEncodedAsync(new { type = "validate", code = "CHAT Hello\nSAY Hi", useValidators = "false" })
                .ReceiveJson<Result>();
            task.Wait();
            Assert.AreEqual(Result.OK, task.Result.Status);
            //Console.WriteLine(task.Result.Data);
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
        public void VisualizeDirect()
        {
            var lines = "CHAT Hello\nSAY Hi";
            var expect = "var chats = {\n  \"1\": \"CHAT Hello\nSAY Hi\",\n};\nvar nodes = new vis.DataSet([\n  { id: 1, label: 'Hello' },\n]);\nvar edges = new vis.DataSet([\n]);";
            var kvs = new Dictionary<string, string>() {
                { "type", "visualize" }, { "code", lines }};
            var result = RequestHandler.Visualize(kvs);
            Assert.That(result, Is.EqualTo(Result.Success(JsonNode.Escape(expect)).ToJSON()));
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
