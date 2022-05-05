using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using KaramelScript.Exceptions;
using KaramelScript.Parser;

namespace KaramelScript
{
	public static class Commands
	{
		//Instructions left to implement
		private static string[] _instructions = new string[]
		{
			"flp",
			"len",
			"tof",
			"put",
			"cut",
			"get",
			
			//Branches group
			"siz",
			"siz!",
			"sin",
			"sin!",
			"jmp",
			"jmp!",
			"end",
			"end!"
		};

		static Commands()
		{
			RegisterBuiltinCommands();
		}

		private static Dictionary<string, Action<Runner.RunnerContext>> _commands;
		
		public static void RegisterBuiltinCommands()
		{
			_commands = new Dictionary<string, Action<Runner.RunnerContext>>();
			
			foreach (var method in typeof(Commands).GetMethods())
			{
				var atrs = method.GetCustomAttributes(typeof(DisplayNameAttribute));
				if (atrs.FirstOrDefault() != null)
				{
					var nameAtr = atrs.First() as DisplayNameAttribute;
					var @delegate = method.CreateDelegate(typeof(Action<Runner.RunnerContext>)) as Action<Runner.RunnerContext>;
					
					_commands.Add(nameAtr.DisplayName, @delegate);
				}
			}
		}

		public static void RunCommand(string name, Runner.RunnerContext context)
		{
			_commands[name].Invoke(context);
		}

		private static void ThrowIfArgumentCountMismatch(string name, int expected, int received)
		{
			if (received != expected)
			{
				throw new ArgumentCountException(name, 1, received);
			}
		}
		
		[DisplayName("new")]
		public static void New(Runner.RunnerContext context)
		{
			var args = context.Current.Children;
			ThrowIfArgumentCountMismatch("new", 2, args.Count);

			context.CreateVariable(args[0].RawValue, args[1].RawValue);
		}
		[DisplayName("set")]
		public static void Set(Runner.RunnerContext context)
		{
			var args = context.Current.Children;
			ThrowIfArgumentCountMismatch("set", 2, args.Count);

			if (args[0] is ReferenceExpression target)
			{
				switch (args[1])
				{
					case ReferenceExpression reference:
					{
						var targetVar = context.GetVariable(target.RawValue);
						var sourceVar = context.GetVariable(reference.RawValue);
						context.CopyVariable(targetVar, sourceVar);
					}
						break;
					case LiteralExpression literal:
					{
						var targetVar = context.GetVariable(target.RawValue);
						
						if (literal.Type == "int")
						{
							targetVar.RawValue = int.Parse(literal.RawValue);
						}
						else
						{
							targetVar.RawValue = literal.RawValue;
						}
					}
						break;
				}
			}
		}
		[DisplayName("out")]
		public static void Out(Runner.RunnerContext context)
		{
			var args = context.Current.Children.ToArray();
						
			foreach (var arg in args)
			{
				var @out = arg switch
				{
					ReferenceExpression reference => context.GetVariable(reference.RawValue).RawValue,
					LiteralExpression value => value.RawValue,
					_ => ""
				};
				
				Console.Write(@out);
			}
		}
		[DisplayName("out!")]
		public static void OutEndLine(Runner.RunnerContext context)
		{
			var args = context.Current.Children.ToArray();
						
			foreach (var arg in args)
			{
				var @out = arg switch
				{
					ReferenceExpression reference => context.GetVariable(reference.RawValue).RawValue,
					LiteralExpression value => value.RawValue,
					_ => ""
				};
				
				Console.WriteLine(@out);
			}
		}
		[DisplayName("cpy")]
		public static void Cpy(Runner.RunnerContext context)
		{
			var args = context.Current.Children;
			ThrowIfArgumentCountMismatch("cpy", 2, args.Count);

			if (args[0] is ReferenceExpression target && args[1] is ReferenceExpression source)
			{
				context.GetVariable(target.RawValue).RawValue = context.GetVariable(source.RawValue).RawValue;
			}
			else
			{
				throw new Exception("Expected 2 reference types for copy");
			}
		}
		[DisplayName("add")]
		public static void Add(Runner.RunnerContext context)
		{
			var args = context.Current.Children;
			ThrowIfArgumentCountMismatch("add", 2, args.Count);

			if (args[0] is ReferenceExpression target)
			{
				if (args[1] is ReferenceExpression source)
				{
					var value1 = context.GetVariable(target.RawValue).GetValue<int>();
					var value2 = context.GetVariable(target.RawValue).GetValue<int>();
					
					context.GetVariable(target.RawValue).RawValue = value1 + value2;
				}
				else if (args[1] is LiteralExpression value)
				{
					int value2 = int.Parse(value.RawValue);
					int value1 = context.GetVariable(target.RawValue).GetValue<int>();

					context.GetVariable(target.RawValue).RawValue = value1 + value2;
				}
			}
			
		}
		[DisplayName("sub")]
		public static void Sub(Runner.RunnerContext context)
		{
			var args = context.Current.Children;
			ThrowIfArgumentCountMismatch("sub", 2, args.Count);

			if (args[0] is ReferenceExpression target)
			{
				if (args[1] is ReferenceExpression source)
				{
					var value1 = context.GetVariable(target.RawValue).GetValue<int>();
					var value2 = context.GetVariable(source.RawValue).GetValue<int>();
					
					context.GetVariable(target.RawValue).RawValue = value1 - value2;
				}
				else if (args[1] is LiteralExpression value)
				{
					int value2 = int.Parse(value.RawValue);
					int value1 = context.GetVariable(target.RawValue).GetValue<int>();

					context.GetVariable(target.RawValue).RawValue = value1 - value2;
				}
			}
			
		}
		[DisplayName("mul")]
		public static void Mul(Runner.RunnerContext context)
		{
			var args = context.Current.Children;
			ThrowIfArgumentCountMismatch("mul", 2, args.Count);

			if (args[0] is ReferenceExpression target)
			{
				if (args[1] is ReferenceExpression source)
				{
					var value1 = context.GetVariable(target.RawValue).GetValue<int>();
					var value2 = context.GetVariable(source.RawValue).GetValue<int>();
					
					context.GetVariable(target.RawValue).RawValue = value1 * value2;
				}
				else if (args[1] is LiteralExpression value)
				{
					int value2 = int.Parse(value.RawValue);
					int value1 = context.GetVariable(target.RawValue).GetValue<int>();

					context.GetVariable(target.RawValue).RawValue = value1 * value2;
				}
			}
		}
		[DisplayName("div")]
		public static void Div(Runner.RunnerContext context)
		{
			var args = context.Current.Children;
			ThrowIfArgumentCountMismatch("div", 2, args.Count);

			if (args[0] is ReferenceExpression target)
			{
				if (args[1] is ReferenceExpression source)
				{
					var value1 = context.GetVariable(target.RawValue).GetValue<int>();
					var value2 = context.GetVariable(source.RawValue).GetValue<int>();
					
					context.GetVariable(target.RawValue).RawValue = value1 / value2;
				}
				else if (args[1] is LiteralExpression value)
				{
					int value2 = int.Parse(value.RawValue);
					int value1 = context.GetVariable(target.RawValue).GetValue<int>();

					context.GetVariable(target.RawValue).RawValue = value1 / value2;
				}
			}
		}
		[DisplayName("mod")]
		public static void Mod(Runner.RunnerContext context)
		{
			var args = context.Current.Children;
			ThrowIfArgumentCountMismatch("mod", 2, args.Count);
			
			if (args[0] is ReferenceExpression target)
			{
				if (args[1] is ReferenceExpression source)
				{
					var value1 = context.GetVariable(target.RawValue).GetValue<int>();
					var value2 = context.GetVariable(source.RawValue).GetValue<int>();
					
					context.GetVariable(target.RawValue).RawValue = value1 % value2;
				}
				else if (args[1] is LiteralExpression value)
				{
					int value2 = int.Parse(value.RawValue);
					int value1 = context.GetVariable(target.RawValue).GetValue<int>();

					context.GetVariable(target.RawValue).RawValue = value1 % value2;
				}
			}
		}
		[DisplayName("flp")]
		public static void Flp(Runner.RunnerContext context)
		{
			var args = context.Current.Children;
			ThrowIfArgumentCountMismatch("flp", 1, args.Count);
			
			if (args[0] is ReferenceExpression target)
			{
				int value1 = int.Parse(context.GetVariable(target.RawValue).ToString());

				context.GetVariable(target.RawValue).RawValue = value1 * -1;
			}
		}
		[DisplayName("jmp")]
		public static void Jmp(Runner.RunnerContext context)
		{
			var args = context.Current.Children;
			ThrowIfArgumentCountMismatch("jmp", 1, args.Count);

			if (args[0] is ReferenceExpression target)
			{
				context.JumpTo(target.RawValue);
			}
		}
		
	}
}