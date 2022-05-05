using System;
using System.Collections.Generic;
using KaramelScript.Parser;

namespace KaramelScript
{
	public class Runner
	{
		public class RunnerContext
		{
			public Runner Runner { get; }
			public Expression Current => Runner.Current;
			public RunnerContext(Runner runner)
			{
				Runner = runner;
			}

			public Variable GetVariable(string name) => Runner._variables[name];

			public Variable CreateVariable(string name, string type)
			{
				var var = new Variable(name, type);
				Runner._variables.Add(name, var);
				return var;
			}
			
			public void JumpTo(string label) => Runner.index = Runner._labels[label];
			public void Skip() => Runner.index++;

			public void CopyVariable(Variable target, Variable source)
			{
				if (target.Type != source.Type)
				{
					throw new ArgumentException($"Type mismatch between the variables");
				}

				target.RawValue = source.RawValue;
			}
		}
		
		private ProgramExpression _program;
		
		private readonly Dictionary<string, Variable> _variables = new Dictionary<string, Variable>();
		private readonly Dictionary<string, int> _labels = new Dictionary<string, int>();
		
		public Runner(ProgramExpression program)
		{
			_program = program;
		}

		internal Expression Current;

		private int index;
		public void Run()
		{
			index = 0;
			
			var modules = _program.Children;
			if (modules.Count != 1)
			{
				throw new Exception("Unexpected number or modules received");
			}
			
			var statements = modules[0].Children;

			var context = new RunnerContext(this);
			
			while (index < statements.Count)
			{
				Current = statements[index];
				if (Current is CallExpression call)
				{
					Commands.RunCommand(call.RawValue, context);
				}
				else if (Current is LabelDefinitionExpression label)
				{
					_labels.Add(label.RawValue, index);
				}

				index++;
			}
		}
	}
}