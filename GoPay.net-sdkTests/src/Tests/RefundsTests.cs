﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoPay.Common;
using GoPay.Model.Payments;
using GoPay.Model.Payment;
using System.Collections.Generic;

namespace GoPay.Tests
{
    [TestClass()]
    public class RefundsTests
    {

        [TestMethod()]
        public async void GPConnectorTestRefund()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            long id = 3049215286;
            try
            {
                await connector.GetAppTokenAsync();
                var refundResult = connector.RefundPaymentAsync(id, 1000);
                Assert.IsNotNull(refundResult);
                Assert.IsNotNull(refundResult.Id);

                Console.WriteLine("Refund with amount result: {0}", refundResult);
            }
            catch (GPClientException exception)
            {
                Console.WriteLine("CHYBA refundu");
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //Handle
                }
            }
        }
    }
}
