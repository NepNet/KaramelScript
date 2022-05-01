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
		private static string[] _instrctions = new string[]
		{
			"flp",
			"len",
			"tof",
			"put",
			"cut",
			"get",
		};

		private static string[] _branches = new string[]
		{
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

		[DisplayName("new")]
		public static void New(Runner.RunnerContext context)
		{
			var args = context.Current.Children.ToArray();
			if (args.Length != 2)
			{
				throw new ArgumentCountException("new", 2, args.Length);
			}

			context.Variables.Add(args[0].Value, null);
		}
		[DisplayName("set")]
		public static void Set(Runner.RunnerContext context)
		{
			var args = context.Current.Children.ToArray();
			if (args.Length != 2)
			{
				throw new ArgumentCountException("set", 2, args.Length);
			}

			if (args[0] is ReferenceExpression target)
			{
				context.Variables[target.Value] = args[1] switch
				{
					ReferenceExpression reference => context.Variables[reference.Value],
					LiteralExpression value => value.Value,
					_ => context.Variables[target.Value]
				};
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
					ReferenceExpression reference => context.Variables[reference.Value],
					LiteralExpression value => value.Value,
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
					ReferenceExpression reference => context.Variables[reference.Value],
					LiteralExpression value => value.Value,
					_ => ""
				};
				
				Console.WriteLine(@out);
			}
		}
		[DisplayName("cpy")]
		public static void Cpy(Runner.RunnerContext context)
		{
			var args = context.Current.Children.ToArray();
			if (args.Length != 2)
			{
				throw new ArgumentCountException("cpy", 2, args.Length);
			}

			if (args[0] is ReferenceExpression target && args[1] is ReferenceExpression source)
			{
				context.Variables[target.Value] = context.Variables[source.Value];
			}
			else
			{
				throw new Exception("Expected 2 reference types for copy");
			}
		}
		[DisplayName("add")]
		public static void Add(Runner.RunnerContext context)
		{
			var args = context.Current.Children.ToArray();
			if (args.Length != 2)
			{
				throw new ArgumentCountException("add", 2, args.Length);
			}

			if (args[0] is ReferenceExpression target)
			{
				if (args[1] is ReferenceExpression source)
				{
					int value2 = int.Parse(context.Variables[source.Value].ToString());
					int value1 = int.Parse(context.Variables[target.Value].ToString());

					context.Variables[target.Value] = value1 + value2;
				}
				else if (args[1] is LiteralExpression value)
				{
					int value2 = int.Parse(value.Value);
					int value1 = int.Parse(context.Variables[target.Value].ToString());

					context.Variables[target.Value] = value1 + value2;
				}
			}
			
		}
		[DisplayName("sub")]
		public static void Sub(Runner.RunnerContext context)
		{
			var args = context.Current.Children.ToArray();
			if (args.Length != 2)
			{
				throw new ArgumentCountException("sub", 2, args.Length);
			}

			if (args[0] is ReferenceExpression target)
			{
				if (args[1] is ReferenceExpression source)
				{
					int value2 = int.Parse(context.Variables[source.Value].ToString());
					int value1 = int.Parse(context.Variables[target.Value].ToString());

					context.Variables[target.Value] = value1 - value2;
				}
				else if (args[1] is LiteralExpression value)
				{
					int value2 = int.Parse(value.Value);
					int value1 = int.Parse(context.Variables[target.Value].ToString());

					context.Variables[target.Value] = value1 - value2;
				}
			}
			
		}
		[DisplayName("mul")]
		public static void Mul(Runner.RunnerContext context)
		{
			var args = context.Current.Children.ToArray();
			if (args.Length != 2)
			{
				throw new ArgumentCountException("mul", 2, args.Length);
			}

			if (args[0] is ReferenceExpression target)
			{
				if (args[1] is ReferenceExpression source)
				{
					int value2 = int.Parse(context.Variables[source.Value].ToString());
					int value1 = int.Parse(context.Variables[target.Value].ToString());

					context.Variables[target.Value] = value1 * value2;
				}
				else if (args[1] is LiteralExpression value)
				{
					int value2 = int.Parse(value.Value);
					int value1 = int.Parse(context.Variables[target.Value].ToString());

					context.Variables[target.Value] = value1 * value2;
				}
			}
		}
		[DisplayName("div")]
		public static void Div(Runner.RunnerContext context)
		{
			var args = context.Current.Children.ToArray();
			if (args.Length != 2)
			{
				throw new ArgumentCountException("div", 2, args.Length);
			}

			if (args[0] is ReferenceExpression target)
			{
				if (args[1] is ReferenceExpression source)
				{
					int value2 = int.Parse(context.Variables[source.Value].ToString());
					int value1 = int.Parse(context.Variables[target.Value].ToString());

					context.Variables[target.Value] = value1 / value2;
				}
				else if (args[1] is LiteralExpression value)
				{
					int value2 = int.Parse(value.Value);
					int value1 = int.Parse(context.Variables[target.Value].ToString());

					context.Variables[target.Value] = value1 / value2;
				}
			}
		}
		[DisplayName("mod")]
		public static void Mod(Runner.RunnerContext context)
		{
			var args = context.Current.Children.ToArray();
			if (args.Length != 2)
			{
				throw new ArgumentCountException("mod", 2, args.Length);
			}

			if (args[0] is ReferenceExpression target)
			{
				if (args[1] is ReferenceExpression source)
				{
					int value2 = int.Parse(context.Variables[source.Value].ToString());
					int value1 = int.Parse(context.Variables[target.Value].ToString());

					context.Variables[target.Value] = value1 % value2;
				}
				else if (args[1] is LiteralExpression value)
				{
					int value2 = int.Parse(value.Value);
					int value1 = int.Parse(context.Variables[target.Value].ToString());

					context.Variables[target.Value] = value1 % value2;
				}
			}
		}
		
		[DisplayName("flp")]
		public static void Flp(Runner.RunnerContext context)
		{
			var args = context.Current.Children.ToArray();
			if (args.Length != 1)
			{
				throw new ArgumentCountException("flp", 1, args.Length);
			}

			if (args[0] is ReferenceExpression target)
			{
				int value1 = int.Parse(context.Variables[target.Value].ToString());

				context.Variables[target.Value] = value1 * -1;
			}
		}
	}
}