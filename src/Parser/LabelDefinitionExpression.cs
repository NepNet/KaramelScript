namespace KaramelScript.Parser
{
	public class LabelDefinitionExpression : Expression
	{
		public override string RawValue { get; }

		public LabelDefinitionExpression(Expression parent, string name) : base(parent)
		{
			RawValue = name;
		}

		public override string ToString() => $"Label '{RawValue}'";
	}
}