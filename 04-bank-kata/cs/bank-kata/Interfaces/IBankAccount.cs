namespace bank_kata.Interfaces;

public interface IBankAccount
{
	public void Deposit(int amount);
	public void Withdraw(int amount);
	public void PrintStatement();
}