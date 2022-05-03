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
	}
}