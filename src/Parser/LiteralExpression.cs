namespace KaramelScript.Parser
{
	public class LiteralExpression : Expression
	{
		public override string RawValue { get; }
		public string Type { get; }
		
		public LiteralExpression(Expression parent, string type, string value) : base(parent)
		{
			Type = type;
			RawValue = value;
		}

		public override string ToString() => $"Value '{RawValue}'";
	}
}