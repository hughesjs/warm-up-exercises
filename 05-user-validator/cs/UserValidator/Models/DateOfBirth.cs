namespace UserValidator.Models;

public class DateOfBirth
{
	public DateTime UtcDate;

	public DateOfBirth(DateTime dateOfBirth)
	{
		UtcDate = dateOfBirth.ToUniversalTime().Date;
	}
}