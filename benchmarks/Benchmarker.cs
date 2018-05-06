using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Dialogic;

namespace Benchmarks
{
    public class Md5VsSha256
    {
        private const int N = 10000;
        private readonly byte[] data;

        private readonly SHA256 sha256 = SHA256.Create();
        private readonly MD5 md5 = MD5.Create();

        public Md5VsSha256()
        {
            data = new byte[N];
            new Random(42).NextBytes(data);
        }

        [Benchmark]
        public byte[] Sha256() => sha256.ComputeHash(data);

        [Benchmark]
        public byte[] Md5() => md5.ComputeHash(data);
    }

    public class DialogicBenchmarks
    {
        static IDictionary<string, object> globals =
            new Dictionary<string, object>
            {
                { "obj-prop", "dog" },
                { "animal", "dog" },
                { "prep", "then" },
                { "group", "(a|b)" },
                { "cmplx", "($group | $prep)" },
                { "count", 4 },
        };

        ChatRuntime dialogic;
        readonly FileInfo file;

        public DialogicBenchmarks()
        {
            file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "../../../dialogic/data/noglobal.gs");
        }

        [Benchmark]
        public void Run()
        {
            dialogic = new ChatRuntime();
            dialogic.ParseFile(file);
            dialogic.InvokeImmediate(globals);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<DialogicBenchmarks>();
        }
    }
}