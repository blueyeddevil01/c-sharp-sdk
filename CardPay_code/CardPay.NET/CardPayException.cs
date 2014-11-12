using System;

namespace CardPay
{
    /// <summary>
    /// CardPay exception
    /// </summary>
	public class CardPayException : Exception
	{
		public CardPayException (String message) : base(message){}
	}
}

