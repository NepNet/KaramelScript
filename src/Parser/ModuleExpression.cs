namespace KaramelScript.Parser
{
	public class ModuleExpression : Expression
	{
		public override string Value { get; }
		public ModuleExpression(Expression parent, string name) : base(parent)
		{
			Value = name;
		}
	}
}