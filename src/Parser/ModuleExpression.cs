namespace KaramelScript.Parser
{
	public class ModuleExpression : Expression
	{
		public override string RawValue { get; }
		public ModuleExpression(Expression parent, string name) : base(parent)
		{
			RawValue = name;
		}
	}
}