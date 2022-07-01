using System.Text.RegularExpressions;
using UserValidator.Exceptions;
using UserValidator.Interfaces;
using UserValidator.Models;

namespace UserValidator.Validators;

public class EmailValidator: IParameterValidator<Email>
{
	private static readonly Regex EmailRegex = new(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");


	public void Validate(Email email)
	{
		if (!EmailRegex.IsMatch(email.ToString())) throw new InvalidParameterException(nameof(email));
	}
}