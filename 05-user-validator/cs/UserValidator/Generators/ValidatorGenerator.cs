using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using UserValidator.Attributes;
using UserValidator.Interfaces;

namespace UserValidator.Generators;

[Generator]
public class ValidatorGenerator: ISourceGenerator
{
	public void Initialize(GeneratorInitializationContext context)
	{
		context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
	}

	public void Execute(GeneratorExecutionContext context)
	{
		if (context.SyntaxReceiver is not SyntaxReceiver receiver) return;

		List<IMethodSymbol> methodSymbols = new();
		foreach (MethodDeclarationSyntax method in receiver.CandidateMethods)
		{
			SemanticModel model = context.Compilation.GetSemanticModel(method.SyntaxTree);
			IMethodSymbol? methodSymbol = model.GetDeclaredSymbol(method);
			ImmutableArray<AttributeData> attributes = methodSymbol.GetAttributes();
			foreach (AttributeData attribute in attributes)
			{
				if (attribute.AttributeClass.GetType().IsAssignableTo(typeof(IParameterValidator<>)))
				{
					methodSymbols.Add(methodSymbol);
				}
			}
		}
	}

	private string GetValidationString(MethodInfo method)
	{
		StringBuilder builder = new();
		ParameterInfo[] parameters = method.GetParameters();
		if (parameters.Length == 0) return string.Empty;
		foreach (ParameterInfo info in parameters)
		{
			ValidateParameterAttribute? validationAttribute = info.GetCustomAttribute<ValidateParameterAttribute>();
			if (validationAttribute is null) continue;
			builder.Append($"Activator.CreateInstance<{validationAttribute.ValidatorType.Name}>().Validate({info.Name});\n");
		}
		return builder.ToString();
	}
}

class SyntaxReceiver : ISyntaxReceiver
{
	public List<MethodDeclarationSyntax> CandidateMethods { get; } = new();

	/// <summary>
	/// Called for every syntax node in the compilation, we can inspect the nodes and save any information useful for generation
	/// </summary>
	public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
	{
		// any method with at least one attribute is a candidate for property generation
		if (syntaxNode is MethodDeclarationSyntax { AttributeLists.Count: > 0 } methodDeclarationSyntax)
		{
			CandidateMethods.Add(methodDeclarationSyntax);
		}
	}
}