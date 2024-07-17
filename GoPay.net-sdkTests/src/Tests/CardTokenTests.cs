using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoPay.Common;
using GoPay.Model.Payments;
using GoPay.Model.Payment;
using gopay_dotnet_standard_api.src.Model.Payment;
using RestSharp;

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
        public void GPConnectorTestPaymentWithCardToken()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);

            BasePayment basePayment = createBaseCardTokenPayment();
            try
            {
                Payment result = connector.GetAppToken().CreatePayment(basePayment);
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
                Assert.Fail();
            }
        }


        [TestMethod()]
        public void GPConnectorTestCardTokenPaymentStatus()
        {
            long id = 3052269740;

            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            try
            {
                var payment = connector.GetAppToken().PaymentStatus(id);
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
                Assert.Fail();
            }
        }

        // [TestMethod()]
        public void GPConnectorTestCardDetail()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            long cardId = 3824914275;

            try
            {
                Card result = connector.GetAppToken().GetCardDetail(cardId);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.CardId);
                Assert.AreEqual(cardId, result.CardId);
                Assert.AreEqual("Visa Gold", result.CardBrand);
                Console.WriteLine(result.ToString());
            }
            catch (GPClientException ex)
            {
                Console.WriteLine("Card Detail ERROR");
                var err = ex.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {

                }
            }
        }

        // [TestMethod()]
        public void GPConnectorTestCardDelete()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            long cardId = 3824914275;

            try
            {
                RestResponse response = connector.GetAppToken().DeleteCard(cardId);
                Assert.IsNotNull(response.IsSuccessful);
            }
            catch (GPClientException ex)
            {
                Console.WriteLine("Card Detail ERROR");
                var err = ex.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {

                }
            }
        }

    }
}
