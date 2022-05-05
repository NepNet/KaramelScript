namespace KaramelScript
{
	public class Token
	{
		public string File { get; }
		public int Line { get; }
		public int Start { get; }
		public TokenType TokenType { get; }
		public string RawValue { get; }
		public Token(string file, int line, int start, TokenType tokenType, string rawValue)
		{
			File = file;
			Line = line;
			Start = start;
			TokenType = tokenType;
			RawValue = rawValue;
		}

		public override string ToString()
		{
		//	['code', line 0, start 0] TokenType = 'Comment', Value = ';===================='
		return $"['{File}', line {Line}, start {Start}] TokenType = '{TokenType}', Value = '{RawValue}'";
		}
	}
}