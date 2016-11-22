using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoPay.Common;
using GoPay.Model.Payments;

namespace GoPay.Tests
{
    [TestClass()]
    public class GPConnectorTests
    {
        public const string CLIENT_ID = "1744960415";
        public const string CLIENT_SECRET = "h9wyRz2s";
        public const long GOID = 8339303643;
        public const string API_URL = @"https://gw.sandbox.gopay.com/api";

        [TestMethod()]
        public void GPConnectorTest()
        {
            var connector = new GPConnector(API_URL, CLIENT_ID, CLIENT_SECRET);
            connector.GetAppToken();

            Assert.IsNotNull(connector.AccessToken);
            Assert.IsNotNull(connector.AccessToken.Token);
        }

        [TestMethod()]
        public void GPConnectorTestCreatePayment()
        {
            var connector = new GPConnector(API_URL, CLIENT_ID, CLIENT_SECRET);
            BasePayment payment = new BasePayment()
            {
                Currency = Currency.EUR,
                Lang = "ENG",
                OrderNumber = "789456167879",
                Amount = 7500,
                Target = new Target()
                {
                    GoId = GOID,
                    Type = Target.TargetType.ACCOUNT
                },
                Callback = new Callback()
                {
                    NotificationUrl = "https://eshop798456.com/notify",
                    ReturnUrl = "Https://eshop78945.com/return"
                },
                Recurrence = new Recurrence()
                {
                    Cycle = RecurrenceCycle.ON_DEMAND,
                    DateTo = new DateTime(2020, 12, 12)

                },
                Payer = new Payer()
                {
                    Contact = new PayerContact()
                    {
                        Email = "test@test.gopay.cz"
                    },
                    DefaultPaymentInstrument = PaymentInstrument.PAYMENT_CARD
                }

            };

            Payment result = connector.GetAppToken().CreatePayment(payment);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Assert.IsNotNull(result.GwUrl);
        }

        [TestMethod()]
        public void GPConnectorTestStatus()
        {
            long id = 3044158975;
            var connector = new GPConnector(API_URL, CLIENT_ID, CLIENT_SECRET);

            var payment = connector.GetAppToken().PaymentStatus(id);
            Assert.IsNotNull(payment.Id);
        }

        [TestMethod()]
        public void GPConnectorTestRefund()
        {
            var connector = new GPConnector(API_URL, CLIENT_ID, CLIENT_SECRET);
            long id = 3044158975;

            var result = connector.GetAppToken().RefundPayment(id, 7500);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }
    }
}