namespace KaramelScript.Parser
{
	public class ModuleStatement : Statement
	{
		public string Name { get; }
		public ModuleStatement(Statement parent, string name) : base(parent)
		{
			Name = name;
		}

		public override string ToString() => $"Module [{Name}]";
	}
}