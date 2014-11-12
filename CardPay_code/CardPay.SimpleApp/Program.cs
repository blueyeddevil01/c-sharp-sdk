using System;
using System.Collections.Generic;
using System.Threading;
using CardPay.model.request;
using CardPay.model.response;

namespace CardPay.SimpleApp
{
	class MainClass
	{
		public static void Main (string[] args) {
            //Простейший обработчик вызовов API для примера
            var listenerExample = new CardPayListenerExample();

            //Инициализация CardPay API
            var api = new CardPayAPI("RxzX58Z0p1Hg", 1598, listenerExample);

            //Формирование заказа
		    OrderRequest order;
		    try {
		        order = new OrderRequest {
		            Number = "CardPay.NET Number",
		            Description = "CardPayAPI desciption",
		            Amount = 0.01m,
		            EMail = "f1restarter@rocketmail.com",
		            IsGateway = true,
		            Currency = "USD",
		            Items = new List<Item> {
		                new Item {
		                    Name = "T-Shirt-A",
		                    Description = "Best ever seen T-Shirt",
		                    Price = 25
		                }
		            },
		            Shipping = new Address {
		                City = "New York",
		                State = "NY",
		                Country = "USA",
		                Phone = "+1 (212) 210-2100",
		                Street = "450 W. 33 Street",
		                ZIP = "10001"
		            },
		            Billing = new Address {
		                City = "New York",
		                State = "NY",
		                Country = "USA",
		                Phone = "+1 (212) 210-2100",
		                Street = "450 W. 33 Street",
		                ZIP = "10001"
		            },
		            Card = new Card {Number = "5413330000000027", Holder = "Joe Black", CVV = "598", Expires = "01/2015"}
		        };
		    } catch (CardPayException e) {
		        Console.WriteLine("CardPay error: {0}", e.Message);
		        return;
		    }

            //Получение печатной формы
		    api.PrintForm(order);
            //Оплата
            api.Pay(order);

            //Получение транзакции #621640
            api.GetTransactionsInfo("dev.cardpay.com", "6Zl1JDo3Gx0w", orderNumber: "621640");
            //Получение последних 10 транзакций
            api.GetTransactionsInfo("dev.cardpay.com", "6Zl1JDo3Gx0w");
            //Получение транзакций с 1 января 2014 года
            api.GetTransactionsInfo("dev.cardpay.com", "6Zl1JDo3Gx0w", dateBegin : new DateTime(2014, 01, 01).ToString("yyyy-MM-dd"));
            //Получение транзакций с 1 января 2014 года по 5 января 2014 года
            api.GetTransactionsInfo("dev.cardpay.com", "6Zl1JDo3Gx0w", dateBegin: new DateTime(2014, 01, 01).ToString("yyyy-MM-dd"), dateEnd: new DateTime(2014, 01, 05).ToString("yyyy-MM-dd"));

            //Смена статуса транзакции #621640
            api.ChangeTransactionStatus("dev.cardpay.com", "6Zl1JDo3Gx0w", 621640, TransactionStatus.VOID);
            //Смена статуса транзакции #621640 с необязательными параметрами
            api.ChangeTransactionStatus("dev.cardpay.com", "6Zl1JDo3Gx0w", 621640, TransactionStatus.VOID, amount : 0.01m, reason : "Test change status");

            //Для отработки все асинхронных колбеков
            Thread.Sleep(25000);
		}
	}
}
