using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoPay.Model.Payments;
using GoPay.Model.Payment;

namespace GoPay.Tests
{
    [TestClass()]
    public class RecurrentPaymentTests
    {

        [TestMethod()]
        public async void GPConnectorTestCreateRecurrentPayment()
        {
            var connector = TestUtils.CreateClient();

            BasePayment basePayment = CreatePaymentTests.createBasePayment();

            Recurrence recurrence = new Recurrence()
            {
                Cycle = RecurrenceCycle.ON_DEMAND,
                DateTo = new DateTime(2022, 4, 1)
            };

            basePayment.Recurrence = recurrence;

            try
            {
                await connector.GetAppTokenAsync();
                Payment result = await connector.CreatePaymentAsync(basePayment);
              
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);

                Console.WriteLine("Payment id: {0}", result.Id);
                Console.WriteLine("Payment gw_url: {0}", result.GwUrl);
                Console.WriteLine("Payment instrument: {0}", result.PaymentInstrument);
                Console.WriteLine("Recurrence: {0}", result.Recurrence);

            }
            catch (GPClientException exception)
            {
                Console.WriteLine("Recurrent payment ERROR");
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
            
        }


        //[TestMethod()]
        public async void GPConnectorTestVoidRecurrency()
        {
            var connector = TestUtils.CreateClient();
            long id = 3049249619;
            try
            {
                await connector.GetAppTokenAsync();
                var result = connector.VoidRecurrencyAsync(id);
                Assert.IsNotNull(result.Id);

                Console.WriteLine("Void Recurrency result: {0}", result);
            }
            catch (GPClientException exception)
            {
                Console.WriteLine("Void recurrency ERROR");
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
