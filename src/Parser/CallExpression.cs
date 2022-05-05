namespace KaramelScript.Parser
{
	public class CallExpression : Expression
	{
		public override string RawValue { get; }
		private readonly Expression[] _arguments;

		public CallExpression(Expression parent, string name) : base(parent)
		{
			RawValue = name;
		}

		public override string ToString() => $"Call '{RawValue}'";
	}
}