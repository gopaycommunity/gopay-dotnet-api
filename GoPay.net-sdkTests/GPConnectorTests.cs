using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoPay.Common;
using GoPay.Model.Payments;
using GoPay.Model.Payment;
using System.Collections.Generic;

namespace GoPay.Tests
{
    [TestClass()]
    public class GPConnectorTests
    {
        /*
        public const string CLIENT_ID = "1744960415";
        public const string CLIENT_SECRET = "h9wyRz2s";
        public const long GOID = 8339303643;
        */

        public const string CLIENT_ID = "1689337452";
        public const string CLIENT_SECRET = "CKr7FyEE";
        public const long GOID = 8712700986;

        public const string API_URL = @"https://gw.sandbox.gopay.com/api";

        private BasePayment createBasePayment()
        {
            List<AdditionalParam> addParams = new List<AdditionalParam>();
            addParams.Add(new AdditionalParam() { Name = "AdditionalKey", Value = "AdditionalValue" });

            List<OrderItem> addItems = new List<OrderItem>();
            addItems.Add(new OrderItem() { Name = "First Item", Amount = 1700, Count = 1 });

            List<PaymentInstrument> allowedInstruments = new List<PaymentInstrument>();
            allowedInstruments.Add(PaymentInstrument.BANK_ACCOUNT);
            //allowedInstruments.Add(PaymentInstrument.PAYMENT_CARD);

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
                    GoId = GOID,
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


        //[TestMethod()]
        public void GPConnectorTest()
        {
            var connector = new GPConnector(API_URL, CLIENT_ID, CLIENT_SECRET);
            connector.GetAppToken();

            Console.WriteLine("Token expires in: {0}", connector.AccessToken.ExpiresIn);

            Assert.IsNotNull(connector.AccessToken);
            Assert.IsNotNull(connector.AccessToken.Token);
        }

        [TestMethod()]
        public void GPConnectorTestCreatePayment()
        {
            var connector = new GPConnector(API_URL, CLIENT_ID, CLIENT_SECRET);

            BasePayment basePayment = createBasePayment();
           
            try { 
                Payment result = connector.GetAppToken().CreatePayment(basePayment);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);

                Console.WriteLine("Payment id: {0}", result.Id);
                Console.WriteLine("Payment gw_url: {0}", result.GwUrl);
                Console.WriteLine("Payment instrument: {0}", Enum.GetName(typeof(PaymentInstrument), result.PaymentInstrument));

            } catch (GPClientException exception)
            {
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

        //[TestMethod()]
        public void GPConnectorTestCreatePreAuthorizedPayment()
        {
            var connector = new GPConnector(API_URL, CLIENT_ID, CLIENT_SECRET);

            BasePayment basePayment = createBasePayment();
            basePayment.PreAuthorization = true;

            try
            {
                Payment result = connector.GetAppToken().CreatePayment(basePayment);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);

                Console.WriteLine("Payment id: {0}", result.Id);
                Console.WriteLine("Payment gw_url: {0}", result.GwUrl);
                Console.WriteLine("{0}", result.PreAuthorization);

            }
            catch (GPClientException exception)
            {
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

        //[TestMethod()]
        public void GPConnectorTestStatus()
        {
            
            long id = 3048953128;
            var connector = new GPConnector(API_URL, CLIENT_ID, CLIENT_SECRET);
            try {
                Console.WriteLine("Hello world");
                var payment = connector.GetAppToken().PaymentStatus(id);
                Assert.IsNotNull(payment.Id);

                Console.WriteLine("Payment id: {0}", payment.Id);
                Console.WriteLine("Payment gw_url: {0}", payment.GwUrl);
                Console.WriteLine("Payment state: {0}", payment.State);
                Console.WriteLine("Payment instrument: {0}", Enum.GetName(typeof(PaymentInstrument), payment.PaymentInstrument));
                Console.WriteLine("{0}", payment.PreAuthorization);

            } catch (GPClientException ex)
            {
                Console.WriteLine("CHYBA");
                var err = ex.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

      //  [TestMethod()]
        public void GPConnectorTestRefund()
        {
            var connector = new GPConnector(API_URL, CLIENT_ID, CLIENT_SECRET);
            long id = 3044158975;
            try { 
                var result = connector.GetAppToken().RefundPayment(id, 7500);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
            catch (GPClientException exception)
            {
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //Handle
                }
            }
        }

     //  [TestMethod()]
        public void GPConnectorTestInstrumentRoot()
        {

        }

    }
}

/*
Recurrence = new Recurrence()
{
    Cycle = RecurrenceCycle.ON_DEMAND,
    DateTo = new DateTime(2020, 12, 12)

},
*/
