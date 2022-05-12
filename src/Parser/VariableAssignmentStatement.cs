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
			internal Literal(Statement parent, string name, VariableAssignmentType op, string rawValue, string rawType) : base(parent, name, op)
			{
				RawValue = rawValue;
				Type = Variable.GetType(rawType);
				RawType = rawType;
			}
			
			public override string ToString() => $"{Operator} of [{Name}] with Literal type [{Type}], value [{RawValue}] ";
		}
		public class Reference : VariableAssignmentStatement
		{
			public string Source { get; }
			internal Reference(Statement parent, string name, VariableAssignmentType op, string other) : base(parent, name, op)
			{
				Source = other;
			}

			public override string ToString() => $"{Operator} of Value to [{Name}] from [{Source}]";
		}

		public string Name { get; }
		public VariableAssignmentType Operator { get; }
		
		protected VariableAssignmentStatement(Statement parent, string name, VariableAssignmentType op) : base(parent)
		{
			Name = name;
			Operator = op;
		}

		public static VariableAssignmentStatement Create(Statement parent, VariableAssignmentType @operator, CallExpression call)
		{
			if (call.Children.Count == 2)
			{
				if (call.Children[0] is ReferenceExpression target)
				{
					switch (call.Children[1])
					{
						case LiteralExpression literal:
							return new Literal(parent, target.RawValue, @operator, literal.RawValue,
								literal.Type);
						case ReferenceExpression reference:
							return new Reference(parent, target.RawValue, @operator, reference.RawValue);
					}
				}
			}

			throw new Exception("Validation failed");
		}
	}
}