using System.Data;

namespace KaramelScript.Parser
{
	public class CallStatement : Statement
	{
		public class CallArgument : Statement
		{
			public string Value { get; }
			public string Type { get; }
			internal CallArgument(Statement parent, string value, string type) : base(parent)
			{
				Value = value;
				Type = type;
			}

			public override string ToString() => $"Argument of type [{Type}] with Value [{Value}]";
		}
		
		public string Name { get; }
		protected CallStatement(Statement parent, string name) : base(parent)
		{
			Name = name;
		}

		public static CallStatement Create(Statement parent, CallExpression call)
		{
			var statement = new CallStatement(parent, call.RawValue);

			foreach (var callArg in call.Children)
			{
				switch (callArg)
				{
					case ReferenceExpression reference:
						new CallArgument(statement, reference.RawValue, "reference");
						break;
					case LiteralExpression literal:
						new CallArgument(statement, literal.RawValue, "value");
						break;
				}
			}
			
			return statement;
		}

		public override string ToString() => $"Call {Name} with arguments:";
	}
}