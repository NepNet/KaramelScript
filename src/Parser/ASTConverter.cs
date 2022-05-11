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
							case "set" :
								VariableAssignmentStatement.Create(module, call);
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