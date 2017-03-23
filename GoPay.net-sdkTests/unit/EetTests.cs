using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoPay.Common;
using GoPay.Model.Payments;
using GoPay.Model.Payment;
using GoPay.EETProp;
using System.Collections.Generic;

namespace GoPay.Tests

{
    [TestClass()]
    public class EetTests
    {

        private BasePayment createEETBasePayment()
        {
            List<AdditionalParam> addParams = new List<AdditionalParam>();
            addParams.Add(new AdditionalParam() { Name = "AdditionalKey", Value = "AdditionalValue" });

            List<OrderItem> addItems = new List<OrderItem>();
            addItems.Add(new OrderItem() { Name = "Pocitac Item1", Amount = 119990, Count = 1,
                VatRate = VatRate.RATE_4, ItemType = ItemType.ITEM, Ean = "1234567890123",
                ProductURL = @"https://www.eshop123.cz/pocitac" });
            addItems.Add(new OrderItem() { Name = "Oprava Item2", Amount = 19960, Count = 1,
                VatRate = VatRate.RATE_3, ItemType = ItemType.ITEM, Ean = "1234567890189",
                ProductURL = @"https://www.eshop123.cz/pocitac/oprava" });

            List<PaymentInstrument> allowedInstruments = new List<PaymentInstrument>();
            allowedInstruments.Add(PaymentInstrument.BANK_ACCOUNT);
            allowedInstruments.Add(PaymentInstrument.PAYMENT_CARD);

            List<string> swifts = new List<string>();
            swifts.Add("GIBACZPX");
            swifts.Add("RZBCCZPP");

            BasePayment baseEETPayment = new BasePayment()
            {
                Callback = new Callback()
                {
                    ReturnUrl = @"https://eshop123.cz/return",
                    NotificationUrl = @"https://eshop123.cz/notify"
                },

                OrderNumber = "EET4321",
                Amount = 139950,
                Currency = Currency.CZK,
                OrderDescription = "EET4321Description",

                Lang = "CS",

                AdditionalParams = addParams,

                Items = addItems,

                Target = new Target()
                {
                    GoId = TestUtils.GOID_EET,
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

            return baseEETPayment;
        }

        private Payment createEETPaymentObject(GPConnector connector, BasePayment baseEETPayment)
        {
            Payment result = null;

            try
            {
                result = connector.GetAppToken().CreatePayment(baseEETPayment);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);

                Console.WriteLine("EET Payment id: {0}", result.Id);
                Console.WriteLine("EET Payment gw_url: {0}", result.GwUrl);
                Console.WriteLine("EET Payment instrument: {0}", result.PaymentInstrument);
                Console.WriteLine(baseEETPayment.Eet);

            }
            catch (GPClientException exception)
            {
                Console.WriteLine("Create EET payment ERROR");
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }

            return result;
        }

        //[TestMethod()]
        public void GPConnectorTestCreateEETPayment()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID_EET, TestUtils.CLIENT_SECRET_EET);

            BasePayment baseEETPayment = createEETBasePayment();

            EET eet = new EET()
            {
                CelkTrzba = 139950,
                ZaklDan1 = 99165,
                Dan1 = 20825,
                ZaklDan2 = 17357,
                Dan2 = 2603,
                Mena = Currency.CZK
            };

            baseEETPayment.Eet = eet;

            Payment result = createEETPaymentObject(connector, baseEETPayment);
        }

        //[TestMethod()]
        public void GPConnectorTestCreateRecurrentEETPayment()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID_EET, TestUtils.CLIENT_SECRET_EET);

            BasePayment baseEETPayment = createEETBasePayment();

            /*
            Recurrence recurrence = new Recurrence()
            {
                Cycle = RecurrenceCycle.WEEK,
                Period = 1,
                DateTo = new DateTime(2018, 4, 1)
            };
            baseEETPayment.Recurrence = recurrence;
            */

            
            Recurrence onDemandRecurrence = new Recurrence()
            {
                Cycle = RecurrenceCycle.ON_DEMAND,
                DateTo = new DateTime(2018, 4, 1)
            };
            baseEETPayment.Recurrence = onDemandRecurrence;
            

            EET eet = new EET()
            {
                CelkTrzba = 139950,
                ZaklDan1 = 99165,
                Dan1 = 20825,
                ZaklDan2 = 17357,
                Dan2 = 2603,
                Mena = Currency.CZK
            };

            baseEETPayment.Eet = eet;

            Payment result = createEETPaymentObject(connector, baseEETPayment);
            Console.WriteLine(result.Recurrence);
        }

        //[TestMethod()]
        public void GPConnectorTestNextOnDemandEET()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID_EET, TestUtils.CLIENT_SECRET_EET);

            try
            {
                NextPayment nextPayment = new NextPayment()
                {
                    OrderNumber = "EETOnDemand4321",
                    Amount = 2000,
                    Currency = Currency.CZK,
                    OrderDescription = "EETOnDemand4321Description",
                };
                nextPayment.Items.Add(new OrderItem() { Name = "OnDemand Prodlouzena zaruka", Amount = 2000, Count = 1,
                    VatRate = VatRate.RATE_4, ItemType = ItemType.ITEM, Ean = "12345678901234",
                    ProductURL = @"https://www.eshop123.cz/pocitac/prodlouzena_zaruka" });

                EET eet = new EET()
                {
                    CelkTrzba = 2000,
                    ZaklDan1 = 1580,
                    Dan1 = 420,
                    Mena = Currency.CZK
                };
                nextPayment.Eet = eet;

                Payment onDemandEETPayment = connector.GetAppToken().CreateRecurrentPayment(3049250282, nextPayment);

                Console.WriteLine("OnDemand payment id: {0}", onDemandEETPayment.Id);
                Console.WriteLine("OnDemand payment gw_url: {0}", onDemandEETPayment.GwUrl);
                Console.WriteLine("OnDemand EET Payment instrument: {0}", onDemandEETPayment.PaymentInstrument);
                Console.WriteLine("OnDemand recurrence: {0}", onDemandEETPayment.Recurrence);
                Console.WriteLine("OnDemand amount: {0}", onDemandEETPayment.Amount);
                Console.Write(onDemandEETPayment.EetCode);
                Console.WriteLine(nextPayment.Eet);

            }
            catch (GPClientException exception)
            {
                Console.WriteLine("Creating next on demand EET payment ERROR");
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

        [TestMethod()]
        public void GPConnectorTestEETStatus()
        {
            long id = 3049250282;

            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID_EET, TestUtils.CLIENT_SECRET_EET);
            try
            {
                var payment = connector.GetAppToken().PaymentStatus(id);
                Assert.IsNotNull(payment.Id);

                Console.WriteLine("EET Payment id: {0}", payment.Id);
                Console.WriteLine("EET Payment gw_url: {0}", payment.GwUrl);
                Console.WriteLine("EET Payment state: {0}", payment.State);
                Console.WriteLine("EET Payment instrument: {0}", payment.PaymentInstrument);
                Console.WriteLine("EET PreAuthorization: {0}", payment.PreAuthorization);
                Console.WriteLine("EET Recurrence: {0}", payment.Recurrence);
                Console.WriteLine(payment.EetCode);

            }
            catch (GPClientException ex)
            {
                Console.WriteLine("EET Payment status ERROR");
                var err = ex.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

        //[TestMethod()]
        public void GPConnectorTestEETPaymentRefund()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID_EET, TestUtils.CLIENT_SECRET_EET);

            List<OrderItem> refundedItems = new List<OrderItem>();
            refundedItems.Add(new OrderItem()
            {
                Name = "Pocitac Item1",
                Amount = 119990,
                Count = 1,
                VatRate = VatRate.RATE_4,
                ItemType = ItemType.ITEM,
                Ean = "1234567890123",
                ProductURL = @"https://www.eshop123.cz/pocitac"
            });
            refundedItems.Add(new OrderItem()
            {
                Name = "Oprava Item2",
                Amount = 19960,
                Count = 1,
                VatRate = VatRate.RATE_3,
                ItemType = ItemType.ITEM,
                Ean = "1234567890189",
                ProductURL = @"https://www.eshop123.cz/pocitac/oprava"
            });

            EET eet = new EET()
            {
                CelkTrzba = 139950,
                ZaklDan1 = 99165,
                Dan1 = 20825,
                ZaklDan2 = 17357,
                Dan2 = 2603,
                Mena = Currency.CZK
            };

            RefundPayment refundObject = new RefundPayment()
            {
                Amount = 139950,
                Items = refundedItems,
                Eet = eet
            };

            try
            {
                var refundEETPayment = connector.GetAppToken().RefundPayment(3049250113, refundObject);
                Console.WriteLine("EET refund result: {0}", refundEETPayment);
            }
            catch (GPClientException ex)
            {
                Console.WriteLine("EET Payment refund ERROR");
                var err = ex.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

        //[TestMethod()]
        public void GPConnectorTestEETPReceiptFindByFilter()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID_EET, TestUtils.CLIENT_SECRET_EET);

            EETReceiptFilter filter = new EETReceiptFilter()
            {
                //DateFrom = new DateTime(2017, 4, 2),          //testovani reakce na chybu
                //DateTo = new DateTime(2017, 3, 2),            //testovani reakce na chybu
                DateFrom = new DateTime(2017, 3, 2),
                DateTo = new DateTime(2017, 4, 2),
                IdProvoz = 11
            };

            try
            {
                List<EETReceipt> receipts = connector.GetAppToken().FindEETReceiptsByFilter(filter);

                foreach(EETReceipt currReceipt in receipts)
                {
                    Console.WriteLine(currReceipt);
                }
                Console.WriteLine(receipts.Count);
            }
            catch (GPClientException ex)
            {
                Console.WriteLine("EET Receipt by filter ERROR");
                var err = ex.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

        //[TestMethod()]
        public void GPConnectorTestEETPReceiptFindByPaymentId()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID_EET, TestUtils.CLIENT_SECRET_EET);

            try
            {
                List<EETReceipt> receipts = connector.GetAppToken().GetEETReceiptByPaymentId(3049205133);
                //List<EETReceipt> receipts = connector.GetAppToken().GetEETReceiptByPaymentId(304920);         //testovani reakce na chybu

                foreach (EETReceipt currReceipt in receipts)
                {
                    Console.WriteLine(currReceipt);
                }
                Console.WriteLine(receipts.Count);
            }
            catch (GPClientException ex)
            {
                Console.WriteLine("EET Receipt by payment ID ERROR");
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
