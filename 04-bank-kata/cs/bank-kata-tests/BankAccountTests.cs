using System;
using System.IO;
using System.Text;
using bank_kata;
using bank_kata.Interfaces;
using bank_kata.Models;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Shouldly;
using Xunit;

namespace bank_kata_tests;

public class BankAccountTests
{
	private readonly IBankAccount _account;
	private readonly ITransactionRepository _transactionRepository;
	private readonly Stream _stream;

	public BankAccountTests()
	{
		_stream = Substitute.For<Stream>();
		_transactionRepository = Substitute.For<ITransactionRepository>();
		_account = new BankAccount(_stream, _transactionRepository);
	}
	
	[Fact]
	public void ShouldOutputCorrectStatement()
	{
		const string expectedString = "DATE       | AMOUNT  | BALANCE\n10/04/2014 | 500.00  | 1400.00\n02/04/2014 | -100.00 | 900.00\n01/04/2014 | 1000.00 | 1000.00";

		byte[] expectedBytes = Encoding.UTF8.GetBytes(expectedString);
		
		_stream.Write(Arg.Do<byte[]>(b =>
		{
			b.ShouldBeEquivalentTo(expectedBytes);
		}), 0, expectedBytes.Length);
		
		_account.PrintStatement();
		
		_stream.Received(Quantity.Exactly(1)).Write(Arg.Any<byte[]>(), 0, expectedBytes.Length);
	}

	[Fact]
	public void CanAddWithdrawal()
	{
		const int amount = 100;

		_transactionRepository.AddTransaction(Arg.Do<Transaction>(t =>
		{
			t.Amount.ShouldBe(-amount);
			(DateTime.UtcNow - t.TxTime).TotalSeconds.ShouldBeLessThan(1); // Essentially now
		}));
		
		_account.Withdraw(amount);
		
		_transactionRepository.Received(Quantity.Exactly(1)).AddTransaction(Arg.Any<Transaction>());
	}
	
	[Fact]
	public void CanAddDeposit()
	{
		const int amount = 100;
		
		_transactionRepository.AddTransaction(Arg.Do<Transaction>(t =>
		{
			t.Amount.ShouldBe(amount);
			(DateTime.UtcNow - t.TxTime).TotalSeconds.ShouldBeLessThan(1); // Essentially now
		}));
		
		_account.Deposit(amount);
		
		_transactionRepository.Received(Quantity.Exactly(1)).AddTransaction(Arg.Any<Transaction>());
	}
}