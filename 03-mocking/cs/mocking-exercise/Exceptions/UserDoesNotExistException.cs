namespace mocking_exercise.Exceptions;

public class UserDoesNotExistException: Exception
{
	public UserDoesNotExistException() : base("User does not exist!"){}
}