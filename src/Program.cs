using System;
using System.Globalization;
using System.IO;
using KaramelScript.Lexer;
using KaramelScript.Parser;

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
				
				var program = new ProgramExpression(null);
				var module = new ModuleExpression(program, "name");
				
				new AstBuilder().BuildAST(module, tokens, "a");
				
				void RecursivePrint(Expression thing, int depth)
				{
					Console.WriteLine($"{depth.ToString().PadRight(depth, '\t')} {thing}");
					foreach (var child in thing.Children)
					{
						RecursivePrint(child, depth + 1);
					}
				}
				
				RecursivePrint(program, 0);

				var stat = new ASTConverter().Convert(module);
				
				void RecursiveStatementPrint(Statement thing, int depth)
				{
					Console.WriteLine($"{depth.ToString().PadRight(depth, '\t')} {thing}");
					foreach (var child in thing.Children)
					{
						RecursiveStatementPrint(child, depth + 1);
					}
				}
				
				RecursiveStatementPrint(stat, 0);
				
				/*
				Runner runner = new Runner(program);
				runner.Run();*/
				
			}
			else
			{
				Console.WriteLine("No args given");
			}
		}
	}
}
