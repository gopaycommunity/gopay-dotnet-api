using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoPay.Common;
using GoPay.Model.Payments;
using GoPay.Model.Payment;
using System.Collections.Generic;

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

                Lang = Lang.CS,

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
        public void GPConnectorTestCreatePayment()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);

            connector.IncomingDataEventHandler += incomingDataListener;

            BasePayment basePayment = createBasePayment();
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

        private void incomingDataListener(object sender, GPConnector.ServerHandlerData data)
        {
            var body = System.Text.Encoding.UTF8.GetString(data.Body, 0, data.Body.Length);
            Console.WriteLine($"{data.HttpStatusCode} {body}");
        }

        [TestMethod()]
        public void GPCOnnectorTestPaymentStatis()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            BasePayment basePayment = createBasePayment();

            try
            {
                Payment result = connector.GetAppToken().CreatePayment(basePayment);
                Payment payment = connector.GetAppToken().PaymentStatus(result.Id);
                
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
                Assert.Fail();
            }
        }
    }
}
