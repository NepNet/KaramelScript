namespace KaramelScript.Parser
{
	public class ProgramExpression : Expression
	{
		public override string Value => nameof(ProgramExpression);
		public ProgramExpression(Expression parent) : base(parent)
		{
			
		}
	}
}