namespace KaramelScript
{
	public class Instruction
	{
		public class Argument
		{
			public TokenType[] ExpectedTypes;

			public Argument(params TokenType[] types)
			{
				ExpectedTypes = types;
			}
		}

		public string Name { get; }
		public Argument[] Arguments { get; }

		public Instruction(string name, params Argument[] arguments)
		{
			Name = name;
			Arguments = arguments;
		}
	}
}
