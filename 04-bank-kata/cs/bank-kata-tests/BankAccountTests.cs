using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using bank_kata;
using bank_kata.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Shouldly;
using Xunit;

namespace bank_kata_tests;

public class BankAccountTests
{
	private readonly IBankAccount _account;
	private readonly Stream _stream;

	public BankAccountTests()
	{
		_stream = Substitute.For<Stream>();
		_account = new BankAccount(_stream);
	}
	
	[Fact]
	public void ShouldOutputCorrectStatement()
	{
		const string expectedString = "DATE       | AMOUNT  | BALANCE\n10/04/2014 | 500.00  | 1400.00\n02/04/2014 | -100.00 | 900.00\n01/04/2014 | 1000.00 | 1000.00";

		byte[] expectedBytes = Encoding.UTF8.GetBytes(expectedString);
		
		_account.PrintStatement();
		
		_stream.Received(Quantity.Exactly(1)).Write(Arg.Do<byte[]>(b =>
		{
			b.ShouldBeEquivalentTo(expectedBytes);
		}), 0, expectedBytes.Length);

	}
}