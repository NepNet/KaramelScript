using System.Collections.Generic;

namespace KaramelScript.Parser
{
	public class Statement
	{
		public Statement Parent { get; }
		public IReadOnlyList<Statement> Children => _children;
		private List<Statement> _children = new List<Statement>();

		private void AddChild(Statement expression)
		{
			_children.Add(expression);
		}

		protected Statement(Statement parent)
		{
			Parent = parent;
			Parent?.AddChild(this);
		}
	}
}