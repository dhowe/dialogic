using System;
using System.Collections.Generic;
using Dialogic.NewServer;
using NUnit.Framework;

namespace Dialogic.Test
{

    [TestFixture]
    public class ServerTests : GenericTests
    {
        [Test]
        public void ErrorTests()
        {
            //Assert.Throws<Exception>(() => Operator.SW.Invoke(null, "hello"));

            var lines = "CHAT Hello\nSAY Hi";
            var expect = "{ \"status\": \"ERROR\", \"lineNo\":1, \"data\": \"Line 1 :: CHAT Hello\n\nValidator: missing required meta-key 'type', 'noStart' or 'preload'\" }";
            var kvs = new Dictionary<string, string>() {
                { "type", "validate" }, { "code", lines }, { "useValidators", "true" }};
            var result = RequestHandler.Validate(kvs);
            //Console.WriteLine(result);
            Assert.That(result, Is.EqualTo(expect));
        }

        [Test]
        public void ValidateTests()
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
        public void VisualizeTests()
        {
            var lines = "CHAT Hello\nSAY Hi";
            var expect = "\nvar chats = {\n  \"1\": \"CHAT Hello\\nSAY Hi\",\n};\n\nvar nodes = new vis.DataSet([\n  { id: 1, label: 'Hello' },\n]);\n\nvar edges = new vis.DataSet([\n]);\n";
            var kvs = new Dictionary<string, string>() {
                { "type", "visualize" }, { "code", lines }};
            var result = RequestHandler.Visualize(kvs);
            Assert.That(result, Is.EqualTo(expect));
        }


        [Test]
        public void ExecuteTests()
        {
            var kvs = new Dictionary<string, string>() {
                { "type", "execute" }, { "code", "CHAT Hello\nSAY Hi" }};
            Assert.That(RequestHandler.Execute(kvs), Is.EqualTo(JSONResult("Hi")));
        }

        string JSONResult(string data) => 
            "{ \"status\": \"OK\", \"lineNo\":-1, \"data\": \"" + data + "\" }";
    }
}
