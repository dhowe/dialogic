// IronMeta Copyright © Gordon Tisher 2018

using System;
using IronMeta.Generator;
using System.IO;

namespace IronMeta
{
    class Program
    {
        static int Main(string[] args)
        {
            const string message = "IronMeta -n {0} -o {1} {2}";

            try
            {
                var options = Options.Parse(args);
                var inputInfo = new FileInfo(options.InputFile);
                var outputInfo = new FileInfo(options.OutputFile);

                var match = CSharpShell.Process(inputInfo.FullName,
                    outputInfo.FullName, options.Namespace, true);
                                                
                if (match.Success)
                {
                    Console.WriteLine(string.Format(message, options.Namespace,
                        outputInfo.FullName, inputInfo.FullName));
                    return 0;
                }
                else
                {
                    int num, offset;
                    var line = match.MatchState.GetLine(match.MatchState.LastErrorIndex, out num, out offset);
                    Console.Error.WriteLine("{0}({1},{2}): error: {3}", 
                        inputInfo.FullName, num, offset, match.MatchState.LastError);
                    Console.Error.WriteLine("{0}", line);
                    Console.Error.WriteLine("{0}^", new string(' ', offset));
                    return 1;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                if (e.InnerException != null) Console.Error.WriteLine
                    (e.InnerException.Message);
                return 2;
            }
        }
    }
}
