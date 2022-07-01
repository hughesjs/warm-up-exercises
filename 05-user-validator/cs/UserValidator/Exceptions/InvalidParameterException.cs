namespace UserValidator.Exceptions;

public class InvalidParameterException : Exception
{
	public InvalidParameterException(string paramName) : base($"Invalid Parameter: {paramName}") { }
}