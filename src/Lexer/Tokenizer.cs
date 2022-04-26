using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaramelScript.Lexer
{
	public partial class Tokenizer
	{
		private string _code;
		private string _file;

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
						tokens.Add(new Token(_file, line, index - lineStart, TokenType.NewLine, string.Empty));
					}
					//continue;
				}
				else if (IsCommentStart(c))
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
					
					tokens.Add(new Token(_file, line, s - lineStart, TokenType.Comment, value));
				}
				else if(IsLabelStart(c))
				{
					int start = index;
					while (ReadNext(out c))
					{
						if (!IsIdentifierChar(c))
						{
							index--;
							break;
						}
					}
					int end = index;

					string value = _code.Substring(start, end - start);
					
					tokens.Add(new Token(_file, line, start - lineStart, TokenType.Label, value));
				}
				//Long bois
				else if (IsDigitChar(c))
				{
					int start = index - 1;
					while (ReadNext(out c))
					{
						if (!IsDigitChar(c))
						{
							index--;
							break;
						}
					}
					int end = index;

					string value = _code.Substring(start, end - start);
					
					tokens.Add(new Token(_file, line, start - lineStart, TokenType.Literal, value));
				}
				else if (IsIdentifierChar(c))
				{
					int start = index - 1;
					while (ReadNext(out c))
					{
						if (!IsIdentifierChar(c))
						{
							index--;
							break;
						}
					}
					int end = index;

					string value = _code.Substring(start, end - start);
					
					tokens.Add(new Token(_file, line, start - lineStart, TokenType.Identifier, value));
				}
				else if (IsStringStart(c))
				{
					//We don't care about the markers so we can set the start as index instead of index-1
					
					int start = index;
					while (ReadNext(out c))
					{
						//Check if escape character to prevent an escaped string marker to end the scan
						if (c == ESCAPE)
						{
							if (PeekNext(out c))
							{
								//Gotta check if the escape character appears again so it doesn't take the string marker in 
								if (IsStringStart(c) || c == ESCAPE)
								{
									ReadNext(out _);
								}
							}
							continue;
						}
						if (IsStringStart(c))
						{
							break;
						}
					}
					//However at the end we need index-1
					int end = index - 1;

					string value = _code.Substring(start, end - start);

					value = ProcessEscapedCharacters(value);
					
					tokens.Add(new Token(_file, line, start - lineStart, TokenType.Literal, value));
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

		private string ProcessEscapedCharacters(string input)
		{
			var raw = new char[input.Length];
			int outIndex = 0;
			for (
				int inIndex = 0; 
				inIndex < input.Length;
				inIndex++, outIndex++)
			{
				if (input[inIndex] == ESCAPE)
				{
					if (TryGetEscapedCharacter(input[inIndex + 1], out char escaped))
					{
						raw[outIndex] = escaped;
						inIndex++;
					}
				}
				else
				{

					raw[outIndex] = input[inIndex];
				}
			}

			Array.Resize(ref raw, outIndex);
			var output = new string(raw);
			return output;
		}
	}
}