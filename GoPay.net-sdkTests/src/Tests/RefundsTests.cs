﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoPay.Common;
using GoPay.Model.Payments;
using GoPay.Model.Payment;
using System.Collections.Generic;
using gopay_dotnet_standard_api.src.Model.Payment;

namespace GoPay.Tests
{
    [TestClass()]
    public class RefundsTests
    {

        // [TestMethod()]
        public void GPConnectorTestRefund()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            long id = 3166388667;
            try
            {
                var refundResult = connector.GetAppToken().RefundPayment(id, 1700);
                Assert.IsNotNull(refundResult);
                Assert.IsNotNull(refundResult.Id);

                Console.WriteLine("Refund with amount result: {0}", refundResult);
            }
            catch (GPClientException exception)
            {
                Console.WriteLine("Unable to refund payment");
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //Handle
                }
                Assert.Fail();
            }
        }

    //    [TestMethod()]
        public void TestRefundsHistory()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            long id = 3178283550;
            try
            {
                List<Refund> refundResult = connector.GetAppToken().GetRefundsHistory(id);
                Assert.IsNotNull(refundResult);

                Console.WriteLine("Refund history result: {0}", refundResult);
            }
            catch (GPClientException exception)
            {
                Console.WriteLine("Unable to obtain history");
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //Handle
                }
                Assert.Fail();
            }
        }
    }
}
