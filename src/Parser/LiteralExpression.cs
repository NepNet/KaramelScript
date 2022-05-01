namespace KaramelScript.Parser
{
	public class LiteralExpression : Expression
	{
		public override string Value { get; }
		public string Type { get; }
		
		public LiteralExpression(Expression parent, string type, string value) : base(parent)
		{
			Type = type;
			Value = value;
		}

		public override string ToString() => $"Value '{Value}'";
	}
}