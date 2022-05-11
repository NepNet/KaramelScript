using KaramelScript.Parser;

namespace KaramelScript
{
	public class Variable
	{
		public string Name { get; }
		public string Type { get; }
		//public object Value { get; set; }

		private object _value;
		public object RawValue
		{
			get => _value;
			set => _value = value;
		}
		public T GetValue<T>() => (T)_value;
		
		public Variable(string name, string type)
		{
			Name = name;
			Type = type;
		}
		
		public static VariableType GetType(string rawName)
		{
			return rawName switch
			{
				"int" => VariableType.Integer,
				"string" => VariableType.String,
				"bool" => VariableType.Boolean,
				"float" => VariableType.Float,
				_ => VariableType.Custom
			};
		}
	}
}