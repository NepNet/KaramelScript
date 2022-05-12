using System;

namespace KaramelScript.Parser
{
	public class JumpStatement : Statement
	{
		public string Name { get; }
		protected JumpStatement(Statement parent, string name) : base(parent)
		{
			Name = name;
		}

		public override string ToString() => $"Jump to [{Name}]";

		public static JumpStatement Create(Statement parent, Expression call)
		{
			if (call.Children.Count == 1)
			{
				if (call.Children[0] is ReferenceExpression name)
				{
					return new JumpStatement(parent, name.RawValue);
				}
			}
			throw new Exception("Validation failed");
		}
	}
}