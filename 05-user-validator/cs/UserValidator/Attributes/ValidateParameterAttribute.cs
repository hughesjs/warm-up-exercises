using UserValidator.Interfaces;

namespace UserValidator.Attributes;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
public class ValidateParameterAttribute: Attribute
{
	public Type ValidatorType { get; }

	public ValidateParameterAttribute(Type validatorType)
	{
		if (!validatorType.IsAssignableTo(typeof(IParameterValidator<>))) throw new("Must provide a validator!");
	}
}