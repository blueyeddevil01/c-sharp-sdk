using System;

namespace CardPay
{
	public class CardPayException : Exception
	{
		public CardPayException (String message) : base(message)
		{
		}
	}
}

