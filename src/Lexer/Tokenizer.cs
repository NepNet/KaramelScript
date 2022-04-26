using System;
using System.Collections.Generic;
using System.Linq;

namespace KaramelScript.Lexer
{
	public partial class Tokenizer
	{
		private string _code;

		public Tokenizer(string code)
		{
			_code = code;
		}

		public Token[] Process()
		{
			index = 0;
			int line = 0;
			int lineStart = 0;
			
			List<Token> tokens = new List<Token>();
			
			while (ReadNext(out char c))
			{
				if (char.IsWhiteSpace(c))
				{
					if (c == '\n')
					{
						line++;
						lineStart = index;
						tokens.Add(new Token("code", line, index - lineStart, TokenType.NewLine, string.Empty));
					}
					//continue;
				}
				else if (Tokenizer.IsCommentStart(c))
				{
					int s = index - 1;
					while (ReadNext(out c))
					{
						if (c == '\n')
						{
							index--;
							break;
						}
					}
					int e = index;
					
					string value = _code.Substring(s, e - s);
					
					tokens.Add(new Token("code", line, s - lineStart, TokenType.Comment, value));
				}
				else if(Tokenizer.IsLabelStart(c))
				{
					int start = index;
					while (ReadNext(out c))
					{
						if (!Tokenizer.IsIdentifierChar(c))
						{
							index--;
							break;
						}
					}
					int end = index;

					string value = _code.Substring(start, end - start);
					
					tokens.Add(new Token("code", line, start - lineStart, TokenType.Label, value));
				}
				//Long bois
				else if (Tokenizer.IsDigitChar(c))
				{
					int start = index - 1;
					while (ReadNext(out c))
					{
						if (!Tokenizer.IsDigitChar(c))
						{
							index--;
							break;
						}
					}
					int end = index;

					string value = _code.Substring(start, end - start);
					
					tokens.Add(new Token("code", line, start - lineStart, TokenType.Literal, value));
				}
				else if (Tokenizer.IsIdentifierChar(c))
				{
					int start = index - 1;
					while (ReadNext(out c))
					{
						if (!Tokenizer.IsIdentifierChar(c))
						{
							index--;
							break;
						}
					}
					int end = index;

					string value = _code.Substring(start, end - start);
					
					tokens.Add(new Token("code", line, start - lineStart, TokenType.Identifier, value));
				}
				else if (Tokenizer.IsStringStart(c))
				{
					//We don't care about the markers so we can set the start as index instead of index-1
					int start = index;
					while (ReadNext(out c))
					{
						if (Tokenizer.IsStringStart(c))
						{
							break;
						}
					}
					//However at the end we need index-1
					int end = index - 1;

					string value = _code.Substring(start, end - start);
					
					tokens.Add(new Token("code", line, start - lineStart, TokenType.Literal, value));
				}
				else
				{
					//Idk, crash something
					throw new NotSupportedException($"Unexpected token '{new string(_code.Skip(index - 1).Take(5).ToArray())}' on line {line} char {index}");
				}
				
			}

			return tokens.ToArray();
		}

		private int index;
		private bool ReadNext(out char c)
		{
			if (index < _code.Length)
			{
				c = _code[index++];
				return true;
			}

			c = '\0';
			return false;
		}
		
		private bool PeekNext(out char c, int offset = 0)
		{
			if (index + offset < _code.Length)
			{
				c = _code[index + offset];
				return true;
			}

			c = '\0';
			return false;
		}
	}
}