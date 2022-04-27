using System.Linq;

namespace KaramelScript
{
	public class Commands
	{
		private static string[] _instrctions = new string[]
		{
			"new",
			"set",
			"out",
			"out!",
			"cpy",
			"add",
			"sub",
			"flp",
			"len",
			"tof",
			"put",
			"cut",
			"get",
			
			//Extras
			"mul",	//Multiply
			"div", //Divide
			"mod", //Modulo
		};

		private static string[] _branches = new string[]
		{
			"siz",
			"siz!",
			"sin",
			"sin!",
			"jmp",
			"jmp!",
			"end",
			"end!"
		};

		public bool IsCommand(Token token)
		{
			if (token.TokenType == TokenType.Identifier)
			{
				return _instrctions.Contains(token.Value) || _branches.Contains(token.Value);
			}
			return false;
		}
	}
}