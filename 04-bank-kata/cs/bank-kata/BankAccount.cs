using System.Globalization;
using System.Text;
using bank_kata.Interfaces;
using bank_kata.Models;

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

		decimal acc = 0;
		IEnumerable<(Transaction, decimal)> transactionsWithBalance = transactions.Select(t =>
		{
			acc += t.Amount;
			return (t, acc);
		}).ToArray();

		StringBuilder builder = new();
		builder.Append(CultureInfo.CurrentCulture, $"{"DATE",-11}| {"AMOUNT",-8}| {"BALANCE",-7}\n");

		foreach ((Transaction Tx, decimal Balance) txBalancePair in transactionsWithBalance.Reverse())
		{
			builder.AppendFormat(CultureInfo.CurrentCulture, $"{txBalancePair.Tx.TxTime,-11:dd/MM/yyyy}| {txBalancePair.Tx.Amount,-8:F2}| {txBalancePair.Balance, -7:F2}\n");
		}

		byte[] bytes = Encoding.UTF8.GetBytes(builder.ToString());
		
		_outputStream.Write(bytes, 0, bytes.Length);
	}
}