namespace KaramelScript.Parser
{
	public class ReferenceExpression : Expression
	{
		public override string Value { get; }
		
		public ReferenceExpression(Expression parent, string name) : base(parent)
		{
			Value = name;
		}

		public override string ToString() => $"Reference to '{Value}'";
	}
}