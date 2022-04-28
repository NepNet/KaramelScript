using System;
using System.Collections.Generic;
using KaramelScript.AST;

namespace KaramelScript
{
	public static class Parser
	{
		public static List<List<Token>> GroupTokens(IEnumerable<Token> input)
		{
			var groups = new List<List<Token>>();
			var group = new List<Token>();
			
			foreach (var token in input)
			{
				if (token.TokenType == TokenType.StatementEnd)
				{
					groups.Add(group);
					group = new List<Token>();
				}
				else
				{
					group.Add(token);
				}
			}

			return groups;
		}

		public static Node GetAST(IEnumerable<Token> tokens)
		{
			throw new NotImplementedException();
		}
	}
}