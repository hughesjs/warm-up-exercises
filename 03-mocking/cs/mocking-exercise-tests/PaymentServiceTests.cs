using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using mocking_exercise.Exceptions;
using mocking_exercise.Interfaces;
using mocking_exercise.Models;
using mocking_exercise.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shouldly;
using Xunit;

namespace mocking_exercise_tests;

public class PaymentServiceTests
{
    private readonly PaymentService _service;
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly IUserRepository _userRepository;
    
    private readonly User _user = new(1, "John Snow");
    private readonly PaymentDetails _details = new("1234 1234 1234 1234", "AA1 1AA", "123");

    public PaymentServiceTests()
    {
        _paymentProcessor = Substitute.For<IPaymentProcessor>();
        _userRepository = Substitute.For<IUserRepository>();
        _service = new(_paymentProcessor, _userRepository);
    }

    [Fact]
    public async Task ThrowsIfUserDoesntExist()
    {
        _userRepository.EnsureUserExists(Arg.Any<User>()).Throws(new UserDoesNotExistException());
        await Should.ThrowAsync<UserDoesNotExistException>(() => _service.ProcessPayment(_user, _details));
    }

    [Fact]
    public async Task ProcessesPaymentIfUserDoesExist()
    {
        _userRepository.EnsureUserExists(Arg.Any<User>()).Returns(Task.CompletedTask);
        await _service.ProcessPayment(_user, _details);
        await _paymentProcessor.Received().ProcessPayment(_user, _details);
    }
}