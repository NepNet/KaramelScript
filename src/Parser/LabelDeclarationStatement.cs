namespace KaramelScript.Parser
{
	public class LabelDeclarationStatement : Statement
	{
		public string Name { get; }
		public LabelDeclarationStatement(Statement parent, string name) : base(parent)
		{
			Name = name;
		}

		public override string ToString() => $"Label [{Name}]";
	}
}