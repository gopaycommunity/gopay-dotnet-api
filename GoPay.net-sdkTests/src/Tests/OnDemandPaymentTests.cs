using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoPay.Common;
using GoPay.Model.Payments;
using GoPay.Model.Payment;

namespace GoPay.Tests

{
    [TestClass()]
    public class OnDemandPaymentTests
    {

        //[TestMethod()]
        public async void GPConnectorTestOnDemand()
        {
            var connector = TestUtils.CreateClient();

            BasePayment basePayment = CreatePaymentTests.createBasePayment();

            Recurrence onDemandRecurrence = new Recurrence()
            {
                Cycle = RecurrenceCycle.ON_DEMAND,
                DateTo = new DateTime(2018, 4, 1)
            };

            basePayment.Recurrence = onDemandRecurrence;

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
                Console.WriteLine("Payment amount: {0}", result.Amount);
            }
            catch (GPClientException exception)
            {
                Console.WriteLine("Creating OnDemand payment ERROR");
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

        [TestMethod()]
        public async void GPConnectorTestNextOnDemand()
        {
            var connector = TestUtils.CreateClient();

            try
            {
                NextPayment nextPayment = new NextPayment()
                {
                    OrderNumber = "OnDemand4321",
                    Amount = 4000,
                    Currency = Currency.CZK,
                    OrderDescription = "OnDemand4321Description",
                };
                nextPayment.Items.Add(new OrderItem() { Name = "OnDemand First Item", Amount = 2000, Count = 2 });

                await connector.GetAppTokenAsync();
                Payment onDemandPayment = await connector.CreateRecurrentPaymentAsync(3049249957, nextPayment);

                Console.WriteLine("OnDemand payment id: {0}", onDemandPayment.Id);
                Console.WriteLine("OnDemand payment gw_url: {0}", onDemandPayment.GwUrl);
                Console.WriteLine("OnDemand Payment instrument: {0}", onDemandPayment.PaymentInstrument);
                Console.WriteLine("OnDemand recurrence: {0}", onDemandPayment.Recurrence);
                Console.WriteLine("OnDemand amount: {0}", onDemandPayment.Amount);

            }
            catch (GPClientException exception)
            {
                Console.WriteLine("Creating next on demand payment ERROR");
                var err = exception.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

    }
}
