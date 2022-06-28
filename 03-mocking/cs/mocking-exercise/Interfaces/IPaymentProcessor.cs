using mocking_exercise.Models;

namespace mocking_exercise.Interfaces;

public interface IPaymentProcessor
{
    public Task ProcessPayment(User user, PaymentDetails paymentDetails);
}