namespace KaramelScript.Lexer
{
	public partial class Tokenizer
	{
		private static bool IsCommentStart(char c) => c == ';';
		private static bool IsDigitChar(char c) => char.IsDigit(c);
		private static bool IsIdentifierChar(char c) => char.IsLetterOrDigit(c) || c == '_' || c == '!';
		private static bool IsStringStart(char c) => c == '\'';
		private static bool IsLabelStart(char c) => c == ':';
	}
}