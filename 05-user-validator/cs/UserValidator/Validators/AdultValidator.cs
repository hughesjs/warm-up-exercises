using UserValidator.Exceptions;
using UserValidator.Interfaces;
using UserValidator.Models;

namespace UserValidator.Validators;

public class AdultValidator: IParameterValidator<DateOfBirth>
{
	public void Validate(DateOfBirth dateOfBirth)
	{
		DateTime today = DateTime.UtcNow.Date;
		int age = today.Year - dateOfBirth.UtcDate.Year;
		if (dateOfBirth.UtcDate > today.AddYears(-age)) // Account for leap years
		{
			age--;
		} 
		if (age < 18) throw new InvalidParameterException("User is not 18");
	}
}