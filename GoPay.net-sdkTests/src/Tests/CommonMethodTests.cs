using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoPay.Common;
using GoPay.Model.Payments;
using GoPay.Account;
using System.Threading.Tasks;

namespace GoPay.Tests
{
    [TestClass()]
    public class CommonMethodTests
    {

        [TestMethod()]
        public async Task GPConnectorTest()
        {
            var connector = TestUtils.CreateClient();
            await connector.GetAppTokenAsync();

            Console.WriteLine("Token expires in: {0}", connector.AccessToken.ExpiresIn);

            Assert.IsNotNull(connector.AccessToken);
            Assert.IsNotNull(connector.AccessToken.Token);
        }


        [TestMethod()]
        public async void GPConnectorTestStatus()
        {
            long id = 3049249619;

            var connector = TestUtils.CreateClient();
            try
            {
                await connector.GetAppTokenAsync();
                var payment = await connector.PaymentStatusAsync(id);
                Assert.IsNotNull(payment.Id);

                Console.WriteLine("Payment id: {0}", payment.Id);
                Console.WriteLine("Payment gw_url: {0}", payment.GwUrl);
                Console.WriteLine("Payment state: {0}", payment.State);
                Console.WriteLine("Payment instrument: {0}", payment.PaymentInstrument);
                Console.WriteLine("PreAuthorization: {0}", payment.PreAuthorization);
                Console.WriteLine("Recurrence: {0}", payment.Recurrence);

            }
            catch (GPClientException ex)
            {
                Console.WriteLine("Payment status ERROR");
                var err = ex.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

        [TestMethod()]
        public async void GPConnectorTestPaymentInstrumentRoot()
        {

            var connector = TestUtils.CreateClient();

            try
            {
                await connector.GetAppTokenAsync();
                PaymentInstrumentRoot instrumentsList = await connector.GetPaymentInstruments(TestUtils.GOID, Currency.CZK);
                Assert.IsNotNull(instrumentsList);

                Console.WriteLine("List of enabled payment instruments for shop with go_id: {0} - OK", TestUtils.GOID);
                Console.WriteLine(instrumentsList.ToString());
            }
            catch (GPClientException ex)
            {
                Console.WriteLine("List of enabled payment instruments ERROR");
                var err = ex.Error;
                DateTime date = err.DateIssued;
                foreach (var element in err.ErrorMessages)
                {
                    //
                }
            }
        }

        //[TestMethod()]
        public async void GPConnectorTestStatementGenerating()
        {
            var connector = TestUtils.CreateClient();

            AccountStatement accountStatement = new AccountStatement()
            {
                DateFrom = new DateTime(2017, 1, 1),
                DateTo = new DateTime(2017, 2, 27),
                GoID = TestUtils.GOID,
                Currency = Currency.CZK,
                Format = StatementGeneratingFormat.CSV_A
            };

            try
            {
                await connector.GetAppTokenAsync();
                byte[] statement = await connector.GetStatementAsync(accountStatement);
                Assert.IsNotNull(statement);

                string content = System.Text.Encoding.UTF8.GetString(statement);

                Console.WriteLine("Content of Array to string: {0}", content);
                Console.WriteLine("----------------------------------------------------------------------------------------");

                Console.Write("Byte content: ");
                for (int i = 0; i < statement.Length; i++)
                {
                    Console.Write(" {0}", statement[i]);
                }
            }
            catch (GPClientException ex)
            {
                Console.WriteLine("Generating account statement ERROR");
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