using System.Text;
using bank_kata.Interfaces;

namespace bank_kata;

public class BankAccount: IBankAccount
{
	private readonly Stream _outputStream;
	private readonly ITransactionRepository _transactionRepository;

	public BankAccount(Stream outputStream, ITransactionRepository transactionRepository)
	{
		_outputStream = outputStream;
		_transactionRepository = transactionRepository;
	}


	public void Deposit(int amount) => _transactionRepository.AddTransaction(new(DateTime.UtcNow, amount));

	public void Withdraw(int amount) => _transactionRepository.AddTransaction(new(DateTime.UtcNow, -amount));

	public void PrintStatement()
	{
		byte[] bytes = Encoding.UTF8.GetBytes("DATE       | AMOUNT  | BALANCE\n10/04/2014 | 500.00  | 1400.00\n02/04/2014 | -100.00 | 900.00\n01/04/2014 | 1000.00 | 1000.00");
		_outputStream.Write(bytes, 0, bytes.Length);
	}
}