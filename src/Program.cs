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

                foreach (var token in tokens)
                {
                    Console.WriteLine(token.ToString());
                }
            }
            else
            {
                Console.WriteLine("No args given");
            }
        }
    }
}
