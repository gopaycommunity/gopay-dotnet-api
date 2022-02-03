using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoPay.Common;
using GoPay.Model.Payments;
using GoPay.Model.Payment;
using System.Threading.Tasks;

namespace GoPay.Tests
{
    [TestClass()]
    public class CardTokenTests
    {
        public static BasePayment createBaseCardTokenPayment()
        {
            List<AdditionalParam> addParams = new List<AdditionalParam>();
            addParams.Add(new AdditionalParam() { Name = "AdditionalKey", Value = "AdditionalValue" });

            List<OrderItem> addItems = new List<OrderItem>();
            addItems.Add(new OrderItem() { Name = "First Item", Amount = 4000, Count = 1 });

            List<PaymentInstrument> allowedInstruments = new List<PaymentInstrument>();
            allowedInstruments.Add(PaymentInstrument.PAYMENT_CARD);

            BasePayment basePayment = new BasePayment()
            {
                Callback = new Callback()
                {
                    ReturnUrl = @"https://eshop123.cz/return",
                    NotificationUrl = @"https://eshop123.cz/notify"
                },

                OrderNumber = "4321",
                Amount = 4000,
                Currency = Currency.CZK,
                OrderDescription = "4321Description",

                Lang = "CS",

                AdditionalParams = addParams,

                Items = addItems,

                Target = new Target()
                {
                    GoId = TestUtils.GOID,
                    Type = Target.TargetType.ACCOUNT
                },

                Payer = new Payer()
                {
                    AllowedPaymentInstruments = allowedInstruments,
                    DefaultPaymentInstrument = PaymentInstrument.PAYMENT_CARD,
                    Contact = new PayerContact()
                    {
                        FirstName = "Jarda",
                        LastName = "Sokol",
                        Email = "test-sokol27@test.cz"
                    },
                    AllowedCardToken = "VUHweq2TUuQpgU6UaD4c+123xzUwTBXiZK7jHhW7rhSbUb07XcG69Q0cwTxTYvBG3qyym3sJ5zphQS4vL0kEHvvinxXYMqkZtx4rBA9mtZj9JSpy4cIHkXnH3gR+i6CoQ4M+zI2EXGJ+TQ==",
                    // VerifyPin = ""
                }
            };

            return basePayment;
        }


        [TestMethod()]
        public async Task GPConnectorTestPaymentWithCardToken()
        {
            var connector = TestUtils.CreateClient();
            BasePayment basePayment = createBaseCardTokenPayment();
            try
            {
                await connector.GetAppTokenAsync();
                Payment result = await connector.CreatePaymentAsync(basePayment);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);

                Console.WriteLine("Payment id: {0}", result.Id);
                Console.WriteLine("Payment gw_url: {0}", result.GwUrl);
                Console.WriteLine("Payment instrument: {0}", result.PaymentInstrument);
                Console.WriteLine(result.Payer.Contact);
            }
            catch (GPClientException exception)
            {
                Console.WriteLine("Create payment ERROR");
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
                throw;
            }
        }


        [TestMethod()]
        public async void GPConnectorTestCardTokenPaymentStatus()
        {
            long id = 3052269740;

            var connector = TestUtils.CreateClient();
            try
            {
                await connector.GetAppTokenAsync();
                var payment = await connector.PaymentStatusAsync(id);
                Assert.IsNotNull(payment.Id);

                Console.WriteLine("Payment id: {0}", payment.Id);
                Console.WriteLine("Payment gw_url: {0}", payment.GwUrl);
                Console.WriteLine("Payment state: {0}", payment.State);
                Console.WriteLine("PayerCard - card token: {0}", payment.Payer.PaymendCard.CardToken);
                Console.WriteLine("Payer 3DS Result: {0}", payment.Payer.PaymendCard.ThreeDResult);
            }
            catch (GPClientException ex)
            {
                Console.WriteLine("Payment status ERROR");
                var err = ex.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

    }
}
