using System;
using System.IO;
using KaramelScript.Lexer;

namespace KaramelScript
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                var path = args[0];
                var tokenizer = new Tokenizer(File.ReadAllText(path));

                var tokens = tokenizer.Process();

                var groups = Parser.Parser.GroupTokens(tokens);
                int groupId = 0;
                foreach (var group in groups)
                {
                    Console.WriteLine($">>Group {groupId} -----------------------");
                    foreach (var token in group)
                    {
                        Console.WriteLine(token);
                    }
                    Console.WriteLine($"<<Group {groupId} -----------------------");
                    groupId++;
                }
            }
            else
            {
                Console.WriteLine("No args given");
            }
        }
    }
}
