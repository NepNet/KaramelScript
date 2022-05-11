using System;

namespace KaramelScript.Parser
{
	public class VariableDeclarationStatement : Statement
	{
		public string Name { get; }
		public VariableType Type { get; }
		public string RawType { get; }

		private VariableDeclarationStatement(Statement parent, string name, string rawType) : base(parent)
		{
			Name = name;
			Type = Variable.GetType(rawType);
			RawType = rawType;
		}

		public static VariableDeclarationStatement Create(Statement parent, CallExpression call)
		{
			if (call.Children.Count == 2)
			{
				if (call.Children[0] is ReferenceExpression name &&
				    call.Children[1] is ReferenceExpression type)
				{
					return new VariableDeclarationStatement(parent, name.RawValue, type.RawValue);
				}
			}
			throw new Exception("Validation failed");
		}

		public override string ToString() => $"Variable declaration [{Name}], Type [{Type}], Raw type name [{RawType}]";

		
	}
}