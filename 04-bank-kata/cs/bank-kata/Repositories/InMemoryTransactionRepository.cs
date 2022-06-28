using bank_kata.Interfaces;
using bank_kata.Models;

namespace bank_kata.Repositories;

public class InMemoryTransactionRepository: ITransactionRepository
{

	private readonly List<Transaction> _transactions;

	public InMemoryTransactionRepository() => _transactions = new();

	public void AddTransaction(Transaction transaction) => _transactions.Add(transaction);

	public IReadOnlyList<Transaction> GetTransactions() => _transactions.AsReadOnly();
}