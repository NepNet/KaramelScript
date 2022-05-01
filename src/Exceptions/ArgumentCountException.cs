using System;

namespace KaramelScript.Exceptions
{
	public class ArgumentCountException : Exception
	{
		public override string Message => $"Instruction '{_name}' got {_received} arguments instead of {_expected}";

		private string _name;
		private int _expected;
		private int _received;

		public ArgumentCountException(string name, int expected, int received)
		{
			_name = name;
			_expected = expected;
			_received = received;
		}
	}
}