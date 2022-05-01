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

			public Dictionary<string, object> Variables => Runner.variables;
		}
		
		private ProgramExpression _program;
		
		private Dictionary<string, object> variables = new Dictionary<string, object>();
		
		public Runner(ProgramExpression program)
		{
			_program = program;
		}

		internal Expression Current;
		
		public void Run()
		{
			var modules = _program.Children.ToArray();
			if (modules.Length != 1)
			{
				throw new Exception("Unexpected number or modules received");
			}
			
			var statements = modules[0].Children;

			var context = new RunnerContext(this);
			
			foreach (var statement in statements)
			{
				Current = statement;
				if (statement is CallExpression call)
				{
					Commands.RunCommand(call.Value, context);
				}
				
			}
		}
	}
}