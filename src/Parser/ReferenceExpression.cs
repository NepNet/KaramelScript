namespace KaramelScript.Parser
{
	public class ReferenceExpression : Expression
	{
		public override string RawValue { get; }
		
		public ReferenceExpression(Expression parent, string name) : base(parent)
		{
			RawValue = name;
		}

		public override string ToString() => $"Reference to '{RawValue}'";
	}
}