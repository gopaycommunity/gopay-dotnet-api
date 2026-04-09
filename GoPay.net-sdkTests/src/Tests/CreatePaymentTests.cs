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

            //    allowedInstruments.Add(PaymentInstrument.TWISTO);
            //    allowedInstruments.Add(PaymentInstrument.SKIPPAY);

                    List<string> swifts = new List<string>();
                    swifts.Add("GIBACZPX");
                    swifts.Add("RZBCCZPP");

            /*    List<BnplType> allowedBnplTypes = new List<BnplType>
                {
                    BnplType.DEFERRED_PAYMENT,
                    BnplType.PAY_IN_THREE
                }; */


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
                    // AllowedBnplTypes = allowedBnplTypes,
                    DefaultPaymentInstrument = PaymentInstrument.BANK_ACCOUNT,
                    // DefaultPaymentInstrument = PaymentInstrument.TWISTO,
                    // DefaultBnplType = BnplType.DEFERRED_PAYMENT,
                    // PaymentInstrument = PaymentInstrument.BANK_ACCOUNT,
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
        public void GPConnectorTestPaymentStatus()
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
        // [TestMethod()]
        public void GPConnectorTestGetQrPayment()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            BasePayment basePayment = createBasePayment();

            try
            {
                Payment payment = connector.GetAppToken().CreatePayment(basePayment);
                Assert.IsNotNull(payment);
                Assert.IsNotNull(payment.Id);

                QrPayment qrPayment = connector.GetAppToken().GetQrPayment(payment.Id);
                Assert.IsNotNull(qrPayment);
                Assert.IsTrue(qrPayment.Amount > 0);
                Assert.IsNotNull(qrPayment.QrCode);

                Console.WriteLine("QR Payment amount: {0}", qrPayment.Amount);
                Console.WriteLine("QR Payment currency: {0}", qrPayment.Currency);
                if (qrPayment.Recipient != null)
                {
                    Console.WriteLine("QR Payment recipient name: {0}", qrPayment.Recipient.Name);
                    if (qrPayment.Recipient.BankAccount?.Local != null)
                    {
                        Console.WriteLine("QR Payment local bank account: {0}/{1}", qrPayment.Recipient.BankAccount.Local.AccountNumber, qrPayment.Recipient.BankAccount.Local.BankCode);
                    }
                    if (qrPayment.Recipient.BankAccount?.International != null)
                    {
                        Console.WriteLine("QR Payment IBAN: {0}", qrPayment.Recipient.BankAccount.International.Iban);
                        Console.WriteLine("QR Payment BIC: {0}", qrPayment.Recipient.BankAccount.International.Bic);
                    }
                }
                if (qrPayment.QrCode.Spayd != null)
                    Console.WriteLine("QR Code SPAYD (base64 length): {0}", qrPayment.QrCode.Spayd.Length);
                if (qrPayment.QrCode.PayBySquare != null)
                    Console.WriteLine("QR Code PayBySquare (base64 length): {0}", qrPayment.QrCode.PayBySquare.Length);
                if (qrPayment.QrCode.Sepa != null)
                    Console.WriteLine("QR Code SEPA (base64 length): {0}", qrPayment.QrCode.Sepa.Length);
                if (qrPayment.QrCode.MnbQr != null)
                    Console.WriteLine("QR Code MNB QR (base64 length): {0}", qrPayment.QrCode.MnbQr.Length);
            }
            catch (GPClientException exception)
            {
                Console.WriteLine("Get QR payment ERROR");
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
                Assert.Fail();
            }
        }

        // [TestMethod()]
        public void GPConnectorTestGetQrPaymentSvg()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            BasePayment basePayment = createBasePayment();

            try
            {
                Payment payment = connector.GetAppToken().CreatePayment(basePayment);
                Assert.IsNotNull(payment);
                Assert.IsNotNull(payment.Id);

                QrPayment qrPayment = connector.GetAppToken().GetQrPayment(payment.Id, QrPaymentFormat.svg);
                Assert.IsNotNull(qrPayment);
                Assert.IsTrue(qrPayment.Amount > 0);
                Assert.IsNotNull(qrPayment.QrCode);

                Console.WriteLine("QR Payment (SVG) amount: {0}", qrPayment.Amount);
                Console.WriteLine("QR Payment (SVG) currency: {0}", qrPayment.Currency);
                if (qrPayment.QrCode.Spayd != null)
                    Console.WriteLine("QR Code SPAYD SVG (base64 length): {0}", qrPayment.QrCode.Spayd.Length);
            }
            catch (GPClientException exception)
            {
                Console.WriteLine("Get QR payment SVG ERROR");
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