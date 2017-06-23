using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoPay.Supercash;

namespace GoPay.Tests
{
    [TestClass()]
    public class SupercashTests
    {

        //[TestMethod()]
        public void GPConnectorTestCreateSupercashCoupon()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);

            SupercashCouponRequest couponRequest = new SupercashCouponRequest()
            {
                GoId = TestUtils.GOID,
                SubType = SubType.POSTPAID,
                CustomId = "ID-123457",
                Amount = 100,
                OrderNumber = "1",
                OrderDescription = "Supercash Coupon Test",
                BuyerEmail = "zakaznik@example.com",
                BuyerPhone = "+420777123456",
                DateValidTo = new DateTime(2018, 12, 31),
                NotificiationUrl = "http://www.example-notify.cz/supercash"
            };

            try
            {
                SupercashCoupon result = connector.GetAppToken().CreateSupercashCoupon(couponRequest);
                Assert.IsNotNull(result);

                Console.WriteLine("SC coupon id: {0}", result.SupercashCouponId);
                Console.WriteLine("SC custom id: {0}", result.CustomId);
                Console.WriteLine("SC coupon number: {0}", result.SupercashNumber);
            }
            catch (GPClientException ex)
            {
                Console.WriteLine("Create Supercash Coupon ERROR");
                var err = ex.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

        //[TestMethod()]
        public void GPConnectorTestCreateSupercashCouponBatch()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);

            SupercashBatchRequest batchRequest = new SupercashBatchRequest()
            {
                GoId = TestUtils.GOID,
                BatchNotificationUrl = "http://www.notify.cz/super",
                Defaults = new SupercashBatchItem()
                {
                    SubType = SubType.POSTPAID,
                    Amounts = new List<long> { 300, 400, 500, 600, 700, 800, 900, 1000 },
                    OrderDescription = "Supercash Coupon Batch Test"
                },
                Coupons = new List<SupercashBatchItem> {
                    new SupercashBatchItem()
                    {
                        BuyerEmail = "zakaznik1@example.com",
                        CustomId = "ID-123457",
                        BuyerPhone = "+420777666111",
                        Amounts = new List<long> { 100 }
                    },
                    new SupercashBatchItem()
                    {
                        BuyerEmail = "zakaznik2@example.com",
                        CustomId = "ID-123458",
                        BuyerPhone = "+420777666222",
                        Amounts = new List<long> { 200 }
                    },
                    new SupercashBatchItem()
                    {
                        BuyerEmail = "zakaznik3@example.com",
                        CustomId = "ID-123459",
                        BuyerPhone = "+420777666333",
                        Amounts = new List<long> { 300 }
                    }
                }
            };

            try
            {
                SupercashBatchResult result = connector.GetAppToken().CreateSupercashCouponBatch(batchRequest);
                Assert.IsNotNull(result);
                Console.WriteLine(result.ToString());
            }
            catch (GPClientException ex)
            {
                Console.WriteLine("Create Supercash Coupon Batch ERROR");
                var err = ex.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

        //[TestMethod()]
        public void GPConnectorTestGetSupercashCouponBatchStatus()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            var batchId = 961667719;

            try
            {
                SupercashBatchState result = connector.GetAppToken().GetSupercashCouponBatchStatus(batchId);
                Assert.IsNotNull(result);
                Console.WriteLine(result.ToString());
            }
            catch (GPClientException ex)
            {
                Console.WriteLine("Get Supercash Coupon Batch Status ERROR");
                var err = ex.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

        //[TestMethod()]
        public void GPConnectorTestGetSupercashCouponBatch()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            var batchId = 961667719;

            try
            {
                SupercashBatch result = connector.GetAppToken().GetSupercashCouponBatch(batchId, TestUtils.GOID);
                Assert.IsNotNull(result);
                Console.WriteLine(result.ToString());
            }
            catch (GPClientException ex)
            {
                Console.WriteLine("Get Supercash Coupon Batch ERROR");
                var err = ex.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

        [TestMethod()]
        public void GPConnectorTestFindSupercashCoupons()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            //long[] paymentSessionIds = new long[] { 3050857992, 3050858018 };
            long paymentSessionIds = 3050857992;

            try
            {
                SupercashBatch result = connector.GetAppToken().FindSupercashCoupons(TestUtils.GOID, paymentSessionIds);
                Assert.IsNotNull(result);
                Console.WriteLine(result.ToString());
            }
            catch (GPClientException ex)
            {
                Console.WriteLine("Find Supercash Coupons ERROR");
                var err = ex.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }


        //[TestMethod()]
        public void GPConnectorTestGetSupercashCoupon()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            long couponId = 100154175;

            try
            {
                SupercashPayment result = connector.GetAppToken().GetSupercashCoupon(couponId);
                Assert.IsNotNull(result);
                Console.WriteLine(result.ToString());
            }
            catch (GPClientException ex)
            {
                Console.WriteLine("Get Supercash Coupon ERROR");
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
