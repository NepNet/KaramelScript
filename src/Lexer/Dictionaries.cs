using System.Collections.Generic;

namespace KaramelScript.Lexer
{
	public partial class Tokenizer
	{
		private static bool IsCommentStart(char c) => c == ';';
		private static bool IsDigitChar(char c) => char.IsDigit(c);
		private static bool IsIdentifierChar(char c) => char.IsLetterOrDigit(c) || c == '_' || c == '!';
		private static bool IsStringStart(char c) => c == '\'';
		private static bool IsLabelStart(char c) => c == ':';
		private static bool IsStatementEnd(char c) => c == '\n' || c == ',';

		private const char ESCAPE = '\\';
		private const char NEWLINE = '\n';

		private Dictionary<char, char> _escapedCharacters = new Dictionary<char, char>()
		{
			{'a', '\a'},
			{'b', '\b'},
			{'f', '\f'},
			{'n', '\n'},
			{'r', '\r'},
			{'t', '\t'},
			{'v', '\v'},
			{'\'', '\''},
			{'"', '\"'},
			{'\\', '\\'},
			{'0', '\0'},
		};

		private bool TryGetEscapedCharacter(in char i, out char c) => _escapedCharacters.TryGetValue(i, out c);
	}
}