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

			public Dictionary<string, object> Variables => Runner._variables;
			public void JumpTo(string label) => Runner.index = Runner._labels[label];
		}
		
		private ProgramExpression _program;
		
		private readonly Dictionary<string, object> _variables = new Dictionary<string, object>();
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