namespace KaramelScript.Parser
{
	public class LabelDefinitionExpression : Expression
	{
		public override string Value { get; }

		public LabelDefinitionExpression(Expression parent, string name) : base(parent)
		{
			Value = name;
		}

		public override string ToString() => $"Label '{Value}'";
	}
}