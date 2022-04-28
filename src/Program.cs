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

				var groups = Parser.GroupTokens(tokens);
				/*
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
				}*/
				
				RegisterBuiltinCommands();
				
			}
			else
			{
				Console.WriteLine("No args given");
			}
		}

		private static void RegisterBuiltinCommands()
		{
			//Commands.RegisterInstruction(new Instruction(TokenType.Label, "LabelDeclare", new Instruction.Argument(TokenType.Label)));
			Commands.RegisterInstruction(new Instruction( "new", 
				new Instruction.Argument(TokenType.Identifier),
				new Instruction.Argument(TokenType.Identifier)
				));
			
			Commands.RegisterInstruction(new Instruction("set", 
				new Instruction.Argument(TokenType.Identifier),
				new Instruction.Argument(TokenType.Identifier), new Instruction.Argument(TokenType.Identifier, TokenType.CharacterLiteral, TokenType.NumericLiteral, TokenType.StringLiteral)
			));
			
			Commands.RegisterInstruction(new Instruction("out", 
				new Instruction.Argument(TokenType.Identifier, TokenType.CharacterLiteral, TokenType.NumericLiteral, TokenType.StringLiteral)
			));
			
			Commands.RegisterInstruction(new Instruction("out!", 
				new Instruction.Argument(TokenType.Identifier, TokenType.CharacterLiteral, TokenType.NumericLiteral, TokenType.StringLiteral)
			));
			
			Commands.RegisterInstruction(new Instruction("cpy", 
				new Instruction.Argument(TokenType.Identifier), new Instruction.Argument(TokenType.Identifier)
			));
			
			Commands.RegisterInstruction(new Instruction("add", 
				new Instruction.Argument(TokenType.Identifier),
				new Instruction.Argument(TokenType.Identifier), new Instruction.Argument(TokenType.Identifier, TokenType.NumericLiteral)
			));
			
			Commands.RegisterInstruction(new Instruction("sub", 
				new Instruction.Argument(TokenType.Identifier),
				new Instruction.Argument(TokenType.Identifier), new Instruction.Argument(TokenType.Identifier, TokenType.NumericLiteral)
				));
			
			Commands.RegisterInstruction(new Instruction("mul", 
				new Instruction.Argument(TokenType.Identifier),
				new Instruction.Argument(TokenType.Identifier), new Instruction.Argument(TokenType.Identifier, TokenType.NumericLiteral)
			));
			
			Commands.RegisterInstruction(new Instruction("div", 
				new Instruction.Argument(TokenType.Identifier),
				new Instruction.Argument(TokenType.Identifier), new Instruction.Argument(TokenType.Identifier, TokenType.NumericLiteral)
			));
			
			Commands.RegisterInstruction(new Instruction("mod", 
				new Instruction.Argument(TokenType.Identifier),
				new Instruction.Argument(TokenType.Identifier), new Instruction.Argument(TokenType.Identifier, TokenType.NumericLiteral)
			));
			
			Commands.RegisterInstruction(new Instruction("flp", 
				new Instruction.Argument(TokenType.Identifier)
			));
		}
	}
}