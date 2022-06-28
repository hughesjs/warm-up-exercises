using System.Globalization;
using System.Text;
using bank_kata.Models;

namespace bank_kata.Services;

public static class StatementPrinter
{
	public static void Print(Stream stream, IEnumerable<Transaction> transactions)
	{
		decimal acc = 0;
		IEnumerable<(Transaction, decimal)> transactionsWithBalance = transactions.Select(t =>
		{
			acc += t.Amount;
			return (t, acc);
		});

		StringBuilder builder = new();
		builder.Append(CultureInfo.CurrentCulture, $"{"DATE",-11}| {"AMOUNT",-8}| {"BALANCE",-7}\n");

		foreach ((Transaction Tx, decimal Balance) txBalancePair in transactionsWithBalance.Reverse())
		{
			builder.AppendFormat(CultureInfo.CurrentCulture, $"{txBalancePair.Tx.TxTime,-11:dd/MM/yyyy}| {txBalancePair.Tx.Amount,-8:F2}| {txBalancePair.Balance, -7:F2}\n");
		}

		byte[] bytes = Encoding.UTF8.GetBytes(builder.ToString());
		
		stream.Write(bytes, 0, bytes.Length);
	}
}