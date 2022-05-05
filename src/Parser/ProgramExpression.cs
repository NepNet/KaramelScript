namespace KaramelScript.Parser
{
	public class ProgramExpression : Expression
	{
		public override string RawValue => nameof(ProgramExpression);
		public ProgramExpression(Expression parent) : base(parent)
		{
			
		}
	}
}