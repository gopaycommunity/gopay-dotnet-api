using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoPay.Common;
using GoPay.Model.Payments;
using GoPay.Model.Payment;
using System.Collections.Generic;
using HttpTracer;
using static GoPay.GPConnector;

namespace GoPay.Tests

{
    [TestClass()]
    public class CreatePaymentTests
    {

        public static BasePayment createBasePayment()
        {
            List<AdditionalParam> addParams = new List<AdditionalParam>();
            addParams.Add(new AdditionalParam() { Name = "AdditionalKey", Value = "AdditionalValue" });

            List<OrderItem> addItems = new List<OrderItem>();
            addItems.Add(new OrderItem() { Name = "First Item", Amount = 1700, Count = 1 });

            List<PaymentInstrument> allowedInstruments = new List<PaymentInstrument>();
            allowedInstruments.Add(PaymentInstrument.BANK_ACCOUNT);
            allowedInstruments.Add(PaymentInstrument.PAYMENT_CARD);

            List<string> swifts = new List<string>();
            swifts.Add("GIBACZPX");
            swifts.Add("RZBCCZPP");

            BasePayment basePayment = new BasePayment()
            {
                Callback = new Callback()
                {
                    ReturnUrl = @"https://eshop123.cz/return",
                    NotificationUrl = @"https://eshop123.cz/notify"
                },

                OrderNumber = "4321",
                Amount = 1700,
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
                    AllowedSwifts = swifts,
                    //DefaultPaymentInstrument = PaymentInstrument.BANK_ACCOUNT,
                    //PaymentInstrument = PaymentInstrument.BANK_ACCOUNT,
                    Contact = new PayerContact()
                    {
                        Email = "test@test.gopay.cz"
                    }
                }
            };

            return basePayment;
        }


        [TestMethod()]
        public async void GPConnectorTestCreatePayment()
        {
            var connector = new GPConnector(TestUtils.API_URL, 
                TestUtils.CLIENT_ID, 
                TestUtils.CLIENT_SECRET, 
                true,
                interceptor => new HttpTracerHandler(interceptor, new ConsoleLogger(), HttpMessageParts.All));

            BasePayment basePayment = createBasePayment();
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
            }
        }

        [TestMethod()]
        public async void GPCOnnectorTestPaymentStatis()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            BasePayment basePayment = createBasePayment();

            try
            {
                await connector.GetAppTokenAsync();
                
                Payment result = await connector.CreatePaymentAsync(basePayment);
                Payment payment = await connector.PaymentStatusAsync(result.Id);
                
                

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);

                Console.WriteLine("Payment id: {0}", payment.Id);
                Console.WriteLine("Payment state: {0}", payment.State);
                Console.WriteLine("Payment gw_url: {0}", payment.GwUrl);
                Console.WriteLine("Payment instrument: {0}", payment.PaymentInstrument);
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
            }
        }
    }
}
