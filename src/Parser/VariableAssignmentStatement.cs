using System;

namespace KaramelScript.Parser
{
	public class VariableAssignmentStatement : Statement
	{
		public class Literal : VariableAssignmentStatement
		{
			public string RawValue { get; }
			public VariableType Type { get; }
			public string RawType { get; }
			internal Literal(Statement parent, string name, string rawValue, string rawType) : base(parent, name)
			{
				RawValue = rawValue;
				Type = Variable.GetType(rawType);
				RawType = rawType;
			}
			
			public override string ToString() => $"Assignment of Literal to [{Name}] with type [{Type}], value [{RawValue}]";
		}
		public class Reference : VariableAssignmentStatement
		{
			public string Source { get; }
			internal Reference(Statement parent, string name, string other) : base(parent, name)
			{
				Source = other;
			}

			public override string ToString() => $"Assignment of Value to [{Name}] from [{Source}]";
		}

		public string Name { get; }
		
		protected VariableAssignmentStatement(Statement parent, string name) : base(parent)
		{
			Name = name;
		}

		public static VariableAssignmentStatement Create(Statement parent, CallExpression call)
		{
			if (call.Children.Count == 2)
			{
				if (call.Children[0] is ReferenceExpression target)
				{
					switch (call.Children[1])
					{
						case LiteralExpression literal:
							return new Literal(parent, target.RawValue, literal.RawValue,
								literal.Type);
						case ReferenceExpression reference:
							return new Reference(parent, target.RawValue, reference.RawValue);
					}
				}
			}
			throw new Exception("Validation failed");
		}
	}
}