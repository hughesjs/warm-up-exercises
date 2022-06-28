using mocking_exercise.Models;

namespace mocking_exercise.Interfaces;

public interface IUserRepository
{
    public Task EnsureUserExists(User user);
}