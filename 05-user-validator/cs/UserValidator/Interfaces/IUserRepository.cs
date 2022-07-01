using UserValidator.Models;

namespace UserValidator.Interfaces;

public interface IUserRepository
{
	public void Add(User user);
}