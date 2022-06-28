using System.Globalization;
using System.Text;
using bank_kata.Interfaces;
using bank_kata.Models;
using bank_kata.Services;

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
		IReadOnlyList<Transaction> transactions = _transactionRepository.GetTransactions();
		StatementPrinter.Print(_outputStream, transactions);
	}
}