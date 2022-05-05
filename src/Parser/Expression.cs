using System.Collections.Generic;

namespace KaramelScript.Parser
{
	public abstract class Expression
	{
		public abstract string RawValue { get; }
		public Expression Parent { get; }
		public IReadOnlyList<Expression> Children => _children;
		private List<Expression> _children = new List<Expression>();

		private void AddChild(Expression expression)
		{
			_children.Add(expression);
		}

		protected Expression(Expression parent)
		{
			Parent = parent;
			Parent?.AddChild(this);
		}

		public override string ToString()
		{
			return $"{GetType().Name}";
		}
	}
}