using UserValidator.Attributes;
using UserValidator.Interfaces;
using UserValidator.Models;
using UserValidator.Validators;

namespace UserValidator.Services;

public class UserService
{
	private readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	[ValidatedMethod]
	public User CreateUser(
		[ValidateParameter(typeof(NameValidator))] Name name,
		[ValidateParameter(typeof(EmailValidator))] Email email,
		[ValidateParameter(typeof(AdultValidator))] DateOfBirth dateOfBirth)
	{
		User user = new(name, email, dateOfBirth);
		_userRepository.Add(user);
		return user;
	}
}