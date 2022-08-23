using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoPay.Common;
using GoPay.Model.Payments;
using GoPay.Model.Payment;
using GoPay.Account;
using System.Collections.Generic;

namespace GoPay.Tests
{
    [TestClass()]
    public class CommonMethodTests
    {

        //[TestMethod()]
        public void GPConnectorTest()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            connector.GetAppToken();

            Console.WriteLine("Token expires in: {0}", connector.AccessToken.ExpiresIn);

            Assert.IsNotNull(connector.AccessToken);
            Assert.IsNotNull(connector.AccessToken.Token);
        }


        //[TestMethod()]
        public void GPConnectorTestStatus()
        {
            long id = 3049249619;

            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);
            try
            {
                var payment = connector.GetAppToken().PaymentStatus(id);
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
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GPConnectorTestPaymentInstrumentRoot()
        {

            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);

            try
            {
                PaymentInstrumentRoot instrumentsList = connector.GetAppToken().GetPaymentInstruments(TestUtils.GOID, Currency.CZK);
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
                Assert.Fail();
            }
        }

        //[TestMethod()]
        public void GPConnectorTestStatementGenerating()
        {
            var connector = new GPConnector(TestUtils.API_URL, TestUtils.CLIENT_ID, TestUtils.CLIENT_SECRET);

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
                byte[] statement = connector.GetAppToken().GetStatement(accountStatement);
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
                Assert.Fail();
            }
        }
    }
}