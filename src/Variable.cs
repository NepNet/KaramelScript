using System;

namespace KaramelScript
{
	public class Variable
	{
		public string Name { get; }
		public string Type { get; }
		public object Value { get; set; }

		public Variable(string name, string type)
		{
			Name = name;
			Type = type;
		}
	}
}