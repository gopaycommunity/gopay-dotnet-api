using System;

using GoPay.Model;
using GoPay.Model.Payments;

using RestSharp;
using RestSharp.Authenticators;
using GoPay.Model.Payment;
using Newtonsoft.Json;
using GoPay.Payments;
using System.Threading.Tasks;
using GoPay.Common;
using GoPay.Account;
using System.Collections.Generic;
using GoPay.EETProp;

namespace GoPay
{
    public class GPConnector
    {
        private string APIUrl { get; set; }
        public AccessToken AccessToken { get; set; }
        internal static RestClient Client { get; private set; }
        private string ClientID;
        private string ClientSecret;

        static GPConnector()
        {
            Client = new RestClient();
        }

        public GPConnector(string APIUrl, string clientid, string clientsecret)
        {
            Client.BaseUrl = new Uri(APIUrl);
            ClientID = clientid;         
            ClientSecret = clientsecret;
            Client.UserAgent = "GoPay .NET Client ";
        }

        /// <exception cref="GPClientException"></exception>
        public GPConnector GetAppToken()
        {
            return GetAppToken(OAuth.SCOPE_PAYMENT_ALL);
        }

        /// <exception cref="GPClientException"></exception>
        public GPConnector GetAppToken(string scope)
        {
            var restRequest = new RestSharp.Newtonsoft.Json.RestRequest(@"/oauth2/token", Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("Accept", "application/json");
            restRequest.JsonSerializer.ContentType = "application/x-www-form-urlencoded";
            restRequest.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&scope=" + scope, ParameterType.RequestBody);
            var authenticator = new HttpBasicAuthenticator(ClientID, ClientSecret);
            authenticator.Authenticate(Client, restRequest);
            var response = Client.Execute(restRequest);    
            AccessToken = Deserialize<AccessToken>(response.Content);
            return this;
        }
        

        /// <exception cref="GPClientException"></exception>
        public Payment CreatePayment(BasePayment payment)
        {
            var restRequest = CreateRestRequest(@"/payments/payment");
            restRequest.AddJsonBody(payment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<Payment>(response);
        }

        public async Task<Payment> CreatePaymentAsync(BasePayment payment)
        {
            var restRequest = CreateRestRequest(@"/payments/payment");
            restRequest.AddJsonBody(payment);
            var response = await Client.ExecuteTaskAsync(restRequest);
            return await Task.Factory.StartNew(() => ProcessResponse<Payment>(response));
        }

        /// <exception cref="GPClientException"></exception>
        public PaymentResult RefundPayment(long id, long amount)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/refund");
            restRequest.AddParameter("id",id,ParameterType.UrlSegment);
            restRequest.AddParameter("amount=" + amount, ParameterType.RequestBody);
            var response = Client.Execute<PaymentResult>(restRequest);
            return ProcessResponse<PaymentResult>(response);
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> RefundPaymentAsync(long id, long amount)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/refund");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            restRequest.AddParameter("amount=" + amount, ParameterType.RequestBody);
            var response = await Client.ExecuteTaskAsync(restRequest);
            var result = await Task.Factory.StartNew(() => ProcessResponse<PaymentResult>(response));
            return result;
        }

        /// <exception cref="GPClientException"></exception>
        public PaymentResult RefundPayment(long id, RefundPayment refundPayment)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/refund");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            restRequest.AddJsonBody(refundPayment);
            var response = Client.Execute<PaymentResult>(restRequest);
            return ProcessResponse<PaymentResult>(response);
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> RefundPaymentAsync(long id, RefundPayment refundPayment)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/refund");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            restRequest.AddJsonBody(refundPayment);
            var response = await Client.ExecuteTaskAsync(restRequest);
            var result = await Task.Factory.StartNew(() => ProcessResponse<PaymentResult>(response));
            return result;
        }

        /// <exception cref="GPClientException"></exception>
        public Payment CreateRecurrentPayment(long id, NextPayment nextPayment)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/create-recurrence");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            restRequest.AddJsonBody(nextPayment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<Payment>(response);
        }

        public async Task<Payment> CreateRecurrentPaymentAsync(long id, NextPayment nextPayment)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/create-recurrence");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var content = await Client.ExecuteTaskAsync(restRequest);
            var result = await Task.Factory.StartNew(() => Deserialize<Payment>(content.Content));
            return result;
        }

        /// <exception cref="GPClientException"></exception>
        public PaymentResult VoidRecurrency(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/void-recurrence");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<PaymentResult>(response);
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> VoidRecurrencyAsync(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/void-recurrence");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = await Client.ExecuteTaskAsync(restRequest);
            var result = await Task.Factory.StartNew(() => Deserialize<PaymentResult>(response.Content));
            return result;
        }

        /// <exception cref="GPClientException"></exception>
        public PaymentResult CapturePayment(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/capture");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<PaymentResult>(response);
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> CapturePaymentAsync(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/capture");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = await Client.ExecuteTaskAsync(restRequest);
            return await Task.Factory.StartNew(() => Deserialize<PaymentResult>(response.Content));
        }

        /// <exception cref="GPClientException"></exception>
        public PaymentResult VoidAuthorization(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/void-authorization");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<PaymentResult>(response);
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> VoidAuthorizationAsync(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/void-authorization");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = await Client.ExecuteTaskAsync(restRequest);
            return await Task.Factory.StartNew(() => Deserialize<PaymentResult>(response.Content));
        }

        /// <exception cref="ApplicationException"></exception>
        public Payment PaymentStatus(long id) {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}", null, Method.GET);
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<Payment>(response);
        }

        /// <exception cref="ApplicationException"></exception>
        public async Task<Payment> PaymentStatusAsync(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = await Client.ExecuteTaskAsync(restRequest);
            return await Task.Factory.StartNew(() => Deserialize<Payment>(response.Content));
        }

        /// <exception cref="GPClientException"></exception>
        public PaymentInstrumentRoot GetPaymentInstruments(long goid, Currency currency)
        {
            var restRequest = CreateRestRequest(@"/eshops/eshop/{goid}/payment-instruments/{currency}", null, Method.GET);
            restRequest.AddParameter("goid", goid, ParameterType.UrlSegment);
            restRequest.AddParameter("currency", currency, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<PaymentInstrumentRoot>(response);
        }

        /// <exception cref="GPClientException"></exception>
        public byte[] GetStatement(AccountStatement accountStatement)
        {
            var restRequest = CreateRestRequest(@"/accounts/account-statement");
            restRequest.AddJsonBody(accountStatement);
            var response = Client.Execute(restRequest);
            return ProcessResponse<byte[]>(response);
        }

        /// <exception cref="GPClientException"></exception>
        public List<EETReceipt> FindEETReceiptsByFilter(EETReceiptFilter filter)
        {
            var restRequest = CreateRestRequest(@"/eet-receipts");
            restRequest.AddJsonBody(filter);
            var response = Client.Execute(restRequest);
            return ProcessResponse<List<EETReceipt>>(response);
        }

        /// <exception cref="ApplicationException"></exception>
        public List<EETReceipt> GetEETReceiptByPaymentId(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/eet-receipts", null, Method.GET);
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<List<EETReceipt>>(response);
        }

        private T ProcessResponse<T>(IRestResponse response)
        {
            return Deserialize<T>(response.Content);
        }

        private T Deserialize<T>(string Content)
        {
            var err = JsonConvert.DeserializeObject<APIError>(Content);
            if (err.ErrorMessages != null)
            {
                throw new GPClientException() { Error = err };
            }
            return JsonConvert.DeserializeObject<T>(Content);
        }
        
        private IRestRequest CreateRestRequest(string url)
        {
            return CreateRestRequest(url, null);
        }

        private IRestRequest CreateRestRequest(string url, Parameter parameter, Method method = Method.POST)
        {
            var restRequest = new RestSharp.Newtonsoft.Json.RestRequest(url, method);
            if (parameter != null) { 
                restRequest.AddParameter(parameter);
            }
            restRequest.AddHeader("Accept", "application/json");
            if (method == Method.POST)
            {
                restRequest.AddHeader("Content-Type", "application/json");
            }
            restRequest.AddHeader("Authorization", "Bearer " + AccessToken.Token);
            return restRequest;
        }
    }
}