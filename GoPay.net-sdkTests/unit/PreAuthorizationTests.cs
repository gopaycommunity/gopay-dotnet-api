using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoPay.Common;
using GoPay.Model.Payments;
using GoPay.Model.Payment;
using System.Collections.Generic;

namespace GoPay.Tests

{
    [TestClass()]
    public class PreAuthorizationTests
    {

        //[TestMethod()]
        public void GPConnectorTestCreatePreAuthorizedPayment()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);

            BasePayment basePayment = CreatePaymentTests.createBasePayment();
            basePayment.PreAuthorization = true;

            try
            {
                Payment result = connector.GetAppToken().CreatePayment(basePayment);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);

                Console.WriteLine("Payment id: {0}", result.Id);
                Console.WriteLine("Payment gw_url: {0}", result.GwUrl);
                Console.WriteLine("Payment instrument: {0}", result.PaymentInstrument);
                Console.WriteLine("PreAuthorization: {0}", result.PreAuthorization);

            }
            catch (GPClientException exception)
            {
                Console.WriteLine("PreAuthorized payment ERROR");
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

        //[TestMethod()]
        public void GPConnectorTestVoidAuthorization()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            long id = 3049249125;
            try
            {
                var result = connector.GetAppToken().VoidAuthorization(id);
                Assert.IsNotNull(result.Id);

                Console.WriteLine("Void Authorization result: {0}", result);
            }
            catch (GPClientException exception)
            {
                Console.WriteLine("Void authorization ERROR");
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //Handle
                }
            }
        }

        [TestMethod()]
        public void GPConnectorTestCapturePayment()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            long id = 3049249190;
            try
            {
                var result = connector.GetAppToken().CapturePayment(id);
                Assert.IsNotNull(result.Id);

                Console.WriteLine("Capture payment result: {0}", result);
            }
            catch (GPClientException exception)
            {
                Console.WriteLine("Capture payment ERROR");
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
