using System;

namespace KaramelScript.Parser
{
	public class AstBuilder
	{
		public void BuildAST(ModuleExpression module, Token[] tokens, string name)
		{
			int current = 0;
			int depth = -1;
			
			void Walk(Expression parent)
			{
				var token = tokens[current];
				current++;
				depth++;
				switch (token.TokenType)
				{
					case TokenType.StatementEnd:
						depth = -1;
						break;
					case TokenType.NumericLiteral:
						new LiteralExpression(parent, "int", token.RawValue);
						break;
					case TokenType.StringLiteral:
						new LiteralExpression(parent, "string", token.RawValue);
						break;
					case TokenType.Label:
						new LabelDefinitionExpression(parent, token.RawValue);
						break;
					case TokenType.Identifier:
					{
						if (depth == 0)
						{
							var call = new CallExpression(parent, token.RawValue);
							while (token.TokenType != TokenType.StatementEnd)
							{
								Walk(call);
								token = tokens[current];
							}
						}
						else
						{
							new ReferenceExpression(parent, token.RawValue);
						}
					}
						break;
					default:
					{
						Console.WriteLine(token);
						throw new Exception("Parse exception");
					}
				}
			}

			while (current < tokens.Length)
			{
				Walk(module);
			}
		}
	}
}