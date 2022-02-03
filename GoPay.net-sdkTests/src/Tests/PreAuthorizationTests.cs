using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoPay.Model.Payments;
using GoPay.Model.Payment;

namespace GoPay.Tests

{
    [TestClass()]
    public class PreAuthorizationTests
    {
        

        //[TestMethod()]
        public async void GPConnectorTestCreatePreAuthorizedPayment()
        {
            var connector = TestUtils.CreateClient();

            BasePayment basePayment = CreatePaymentTests.createBasePayment();
            basePayment.PreAuthorization = true;

            try
            {
                await connector.GetAppTokenAsync();
                Payment result = await connector.CreatePaymentAsync(basePayment);
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
        public async void GPConnectorTestVoidAuthorization()
        {
            var connector = TestUtils.CreateClient();
            long id = 3049249125;
            try
            {
                await connector.GetAppTokenAsync();
                var result = await connector.VoidAuthorizationAsync(id);
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
        public async void GPConnectorTestCapturePayment()
        {
            var connector = TestUtils.CreateClient();
            long id = 3049249190;
            try
            {
                await connector.GetAppTokenAsync();
                var result = connector.CapturePaymentAsync(id);
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
