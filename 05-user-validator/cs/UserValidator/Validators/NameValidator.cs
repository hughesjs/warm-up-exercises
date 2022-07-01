using UserValidator.Exceptions;
using UserValidator.Interfaces;
using UserValidator.Models;

namespace UserValidator.Validators;

public class NameValidator: IParameterValidator<Name>
{
	public void Validate(Name name)
	{
		if (string.IsNullOrWhiteSpace(name.FirstName)) throw new InvalidParameterException(nameof(name.FirstName));
		if (string.IsNullOrWhiteSpace(name.LastName)) throw new InvalidParameterException(nameof(name.LastName));
	}
}