using bank_kata.Models;

namespace bank_kata.Interfaces;

public interface ITransactionRepository
{
	public void AddTransaction(Transaction transaction);
	public IReadOnlyList<Transaction> GetTransactions(int userId);
}