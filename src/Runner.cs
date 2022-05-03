using System;
using System.Collections.Generic;
using System.Linq;
using KaramelScript.Exceptions;
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

			public Dictionary<string, Variable> Variables => Runner._variables;
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
			
			var modules = _program.Children.ToArray();
			if (modules.Length != 1)
			{
				throw new Exception("Unexpected number or modules received");
			}
			
			var statements = modules[0].Children.ToArray();

			var context = new RunnerContext(this);
			
			while (index < statements.Length)
			{
				Current = statements[index];
				if (Current is CallExpression call)
				{
					Commands.RunCommand(call.Value, context);
				}
				else if (Current is LabelDefinitionExpression label)
				{
					_labels.Add(label.Value, index);
				}

				index++;
			}
		}
	}
}