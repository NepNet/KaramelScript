namespace KaramelScript.Parser
{
	public class CallExpression : Expression
	{
		public override string Value { get; }
		private readonly Expression[] _arguments;

		public CallExpression(Expression parent, string name) : base(parent)
		{
			Value = name;
		}

		public override string ToString() => $"Call '{Value}'";
	}
}