using mocking_exercise.Interfaces;
using mocking_exercise.Models;

namespace mocking_exercise.Services;

public class PaymentService
{
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly IUserRepository _userRepository;

    public PaymentService(IPaymentProcessor paymentProcessor, IUserRepository userRepository)
    {
        _paymentProcessor = paymentProcessor;
        _userRepository = userRepository;
    }

    public async Task ProcessPayment(User user, PaymentDetails paymentDetails)
    {
        await _userRepository.EnsureUserExists(user);
        await _paymentProcessor.ProcessPayment(user, paymentDetails);
    }
}