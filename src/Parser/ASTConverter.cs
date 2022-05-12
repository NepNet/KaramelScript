namespace KaramelScript.Parser
{
	public class ASTConverter
	{
		public ModuleStatement Convert(ModuleExpression source)
		{
			var module = new ModuleStatement(null, source.RawValue);

			var sourceChildren = source.Children;
			foreach (var expression in sourceChildren)
			{
				switch (expression)
				{
					case CallExpression call:
						switch (call.RawValue)
						{
							case "new" :
								VariableDeclarationStatement.Create(module, call);
								break;
							case "jmp" :
								JumpStatement.Create(module, call);
								break;
							case "set" :
								VariableAssignmentStatement.Create(module, VariableAssignmentType.Set ,call);
								break;
							case "add" :
								VariableAssignmentStatement.Create(module, VariableAssignmentType.Add ,call);
								break;
							case "sub" :
								VariableAssignmentStatement.Create(module, VariableAssignmentType.Subtract ,call);
								break;
							case "mul" :
								VariableAssignmentStatement.Create(module, VariableAssignmentType.Multiply ,call);
								break;
							case "div" :
								VariableAssignmentStatement.Create(module, VariableAssignmentType.Divide ,call);
								break;
							case "mod" :
								VariableAssignmentStatement.Create(module, VariableAssignmentType.Modulo ,call);
								break;
							case "pow" :
								VariableAssignmentStatement.Create(module, VariableAssignmentType.Power ,call);
								break;
							default:
								CallStatement.Create(module, call);
								break;
						}
						break;
					case LabelDefinitionExpression label:
						new LabelDeclarationStatement(module, label.RawValue);
						break;
				}
			}
			
			return module;
		}
	}
}