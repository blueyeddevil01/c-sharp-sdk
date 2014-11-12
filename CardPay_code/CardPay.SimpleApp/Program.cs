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
            //���������� ���������� ������� API ��� �������
            var listenerExample = new CardPayListenerExample();

            //������������� CardPay API
            var api = new CardPayAPI("RxzX58Z0p1Hg", 1598, listenerExample);

            //������������ ������
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

            //��������� �������� �����
		    api.PrintForm(order);
            //������
            api.Pay(order);

            //��������� ���������� #621640
            api.GetTransactionsInfo("dev.cardpay.com", "6Zl1JDo3Gx0w", orderNumber: "621640");
            //��������� ��������� 10 ����������
            api.GetTransactionsInfo("dev.cardpay.com", "6Zl1JDo3Gx0w");
            //��������� ���������� � 1 ������ 2014 ����
            api.GetTransactionsInfo("dev.cardpay.com", "6Zl1JDo3Gx0w", dateBegin : new DateTime(2014, 01, 01).ToString("yyyy-MM-dd"));
            //��������� ���������� � 1 ������ 2014 ���� �� 5 ������ 2014 ����
            api.GetTransactionsInfo("dev.cardpay.com", "6Zl1JDo3Gx0w", dateBegin: new DateTime(2014, 01, 01).ToString("yyyy-MM-dd"), dateEnd: new DateTime(2014, 01, 05).ToString("yyyy-MM-dd"));

            //����� ������� ���������� #621640
            api.ChangeTransactionStatus("dev.cardpay.com", "6Zl1JDo3Gx0w", 621640, TransactionStatus.VOID);
            //����� ������� ���������� #621640 � ��������������� �����������
            api.ChangeTransactionStatus("dev.cardpay.com", "6Zl1JDo3Gx0w", 621640, TransactionStatus.VOID, amount : 0.01m, reason : "Test change status");

            //��� ��������� ��� ����������� ��������
            Thread.Sleep(25000);
		}
	}
}
