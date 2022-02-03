using GoPay.Payments;
using GoPay.Common;
using GoPay.Account;
using GoPay.EETProp;
using GoPay.Model;
using GoPay.Model.Payment;
using GoPay.Model.Payments;
using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;

namespace GoPay
{
    public class GPConnector
    {
        private string APIUrl { get; set; }
        public AccessToken AccessToken { get; set; }
        internal static RestClient Client { get; private set; }
        private string ClientID;
        private string ClientSecret;

        public GPConnector(string APIUrl, string clientid, string clientsecret, bool setSecurityProtocol = true, Func<HttpMessageHandler, HttpMessageHandler> httpMessageInterceptor = null)
        {
            if (setSecurityProtocol)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }
            var options = new RestClientOptions();
            if (httpMessageInterceptor != null)
            {
                options.ConfigureMessageHandler = httpMessageInterceptor;
            }
            options.BaseUrl = new Uri(APIUrl);
            options.UserAgent = "GoPay .NET Client";
            
            options.Timeout = 8000;

            ClientID = clientid;
            ClientSecret = clientsecret;

            Client = new RestClient(options);
            // Client.UseNewtonsoftJson();
        }

        /// <exception cref="GPClientException"></exception>
        public async Task GetAppTokenAsync(string scope = OAuth.SCOPE_PAYMENT_ALL)
        {
            var restRequest = new RestRequest(@"/oauth2/token");
            restRequest.AddHeader(KnownHeaders.Accept, "application/json");
            restRequest.AddHeader(KnownHeaders.ContentType, "application/json");
            restRequest.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&scope=" + scope, ParameterType.RequestBody);
            
            var authenticator = new HttpBasicAuthenticator(ClientID, ClientSecret);
            _ = authenticator.Authenticate(Client, restRequest);
            var response = await Client.ExecutePostAsync<AccessToken>(restRequest);
            if (response.IsSuccessful)
            {
                AccessToken = Deserialize<AccessToken>(response.Content);
            } else
            {
                HandleErrorResponse(response.Content);
            }
        }


        public async Task<Payment> CreatePaymentAsync(BasePayment payment)
        {
            var request = new RestRequest(@"/payments/payment").AddJsonBody(payment);
            request.AddHeader(KnownHeaders.Authorization, "Bearer " + AccessToken.Token);
            try { 
                var response = await Client.ExecutePostAsync<Payment>(request);
                return HandleResponse(response);
            } catch (Exception e)
            {
                throw new GPClientException(e.Message);
            }
        }


        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> RefundPaymentAsync(long id, long amount)
        {
            var restRequest = new RestRequest(@"/payments/payment/{id}/refund");
            restRequest.AddHeader(KnownHeaders.ContentType, "multipart/form-data");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            restRequest.AddParameter("amount", amount);
            restRequest.AddHeader(KnownHeaders.Authorization, "Bearer " + AccessToken.Token);
            try
            {
                var response = await Client.ExecutePostAsync<PaymentResult>(restRequest);
                return HandleResponse(response);
            }
            catch (Exception e)
            {
                throw new GPClientException(e.Message);
            }
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> RefundPaymentAsync(long id, RefundPayment refundPayment)
        {
            var restRequest = new RestRequest(@"/payments/payment/{id}/refund").AddJsonBody(refundPayment);
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            restRequest.AddHeader(KnownHeaders.Authorization, "Bearer " + AccessToken.Token);
            try
            {
                var response = await Client.ExecutePostAsync<PaymentResult>(restRequest);
                return HandleResponse(response);
            }
            catch (Exception e)
            {
                throw new GPClientException(e.Message);
            }
        }

        public async Task<Payment> CreateRecurrentPaymentAsync(long id, NextPayment nextPayment)
        {
            var recurrentPayment = new RestRequest(@"/payments/payment/{id}/create-recurrence").AddJsonBody(nextPayment);
            recurrentPayment.AddParameter("id", id, ParameterType.UrlSegment);
            recurrentPayment.AddHeader(KnownHeaders.Authorization, "Bearer " + AccessToken.Token);
            try
            {
                var response = await Client.ExecutePostAsync<Payment>(recurrentPayment);
                return HandleResponse(response);
            }
            catch (Exception e)
            {
                throw new GPClientException(e.Message);
            }
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> VoidRecurrencyAsync(long id)
        {
            var restRequest = new RestRequest(@"/payments/payment/{id}/void-recurrence");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            restRequest.AddHeader(KnownHeaders.Authorization, "Bearer " + AccessToken.Token);
            restRequest.AddHeader(KnownHeaders.ContentType, "application/x-www-form-urlencoded");
            try
            {
                var response = await Client.ExecutePostAsync<PaymentResult>(restRequest);
                return HandleResponse(response);
            }
            catch (Exception e)
            {
                throw new GPClientException(e.Message);
            }
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> CapturePaymentAsync(long id)
        {
            var restRequest = new RestRequest(@"/payments/payment/{id}/capture");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            restRequest.AddHeader(KnownHeaders.Authorization, "Bearer " + AccessToken.Token);
            restRequest.AddHeader(KnownHeaders.ContentType, "application/x-www-form-urlencoded");
            try
            {
                var response = await Client.ExecutePostAsync<PaymentResult>(restRequest);
                return HandleResponse(response);
            }
            catch (Exception e)
            {
                throw new GPClientException(e.Message);
            }
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> VoidAuthorizationAsync(long id)
        {
            var restRequest = new RestRequest(@"/payments/payment/{id}/void-authorization");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            restRequest.AddHeader(KnownHeaders.Authorization, "Bearer " + AccessToken.Token);
            restRequest.AddHeader(KnownHeaders.ContentType, "application/x-www-form-urlencoded");
            try
            {
                var response = await Client.ExecutePostAsync<PaymentResult>(restRequest);
                return HandleResponse(response);
            }
            catch (Exception e)
            {
                throw new GPClientException(e.Message);
            }
        }

        /// <exception cref="ApplicationException"></exception>
        public async Task<Payment> PaymentStatusAsync(long id)
        {
            var restRequest = new RestRequest(@"/payments/payment/{id}");
            restRequest.AddHeader(KnownHeaders.Authorization, "Bearer " + AccessToken.Token);
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            restRequest.AddHeader(KnownHeaders.ContentType, "application/x-www-form-urlencoded");
            try
            {
                var response = await Client.ExecutePostAsync<Payment>(restRequest);
                return HandleResponse(response);
            }
            catch (Exception e)
            {
                throw new GPClientException(e.Message);
            }
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentInstrumentRoot> GetPaymentInstruments(long goid, Currency currency)
        {
            var restRequest = new RestRequest(@"/eshops/eshop/{goid}/payment-instruments/{currency}");
            restRequest.AddParameter("goid", goid, ParameterType.UrlSegment);
            restRequest.AddParameter("currency", currency, ParameterType.UrlSegment);
            restRequest.AddHeader(KnownHeaders.Authorization, "Bearer " + AccessToken.Token);
            var response = await Client.GetAsync<PaymentInstrumentRoot>(restRequest);
            /*
            if (!response.Content.Contains("error_code"))
            {
                response.Content = ProcessPaymentInstrumentRootContent(response.Content);
            }
            */
            return response;
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<byte[]> GetStatementAsync(AccountStatement accountStatement)
        {
            var restRequest = new RestRequest(@"/accounts/account-statement").AddJsonBody(accountStatement);
            restRequest.AddHeader(KnownHeaders.Authorization, "Bearer " + AccessToken.Token);
            try
            {
                var response = await Client.PostAsync<byte[]>(restRequest);
                return response;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            /*
            string content = response.Content.ToString();

            if (content.Contains("error_code"))
            {
                ProcessResponse<APIError>(response);
            }
            */
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<List<EETReceipt>> FindEETReceiptsByFilterAsync(EETReceiptFilter filter)
        {
            var restRequest = new RestRequest(@"/eet-receipts").AddJsonBody(filter);
            restRequest.AddHeader(KnownHeaders.Authorization, "Bearer " + AccessToken.Token);
            try
            {
                var response = await Client.ExecutePostAsync<List<EETReceipt>>(restRequest);
                return HandleResponse(response);
            }
            catch (Exception e)
            {
                throw new GPClientException(e.Message);
            }
        }

        /// <exception cref="ApplicationException"></exception>
        public async Task<List<EETReceipt>> GetEETReceiptByPaymentIdAsync(long id)
        {
            var restRequest = new RestRequest(@"/payments/payment/{id}/eet-receipts");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            restRequest.AddHeader(KnownHeaders.Authorization, "Bearer " + AccessToken.Token);
            
            try
            {
                var response = await Client.ExecutePostAsync<List<EETReceipt>>(restRequest);
                return HandleResponse(response);
            }
            catch (Exception e)
            {
                throw new GPClientException(e.Message);
            }
        }

        /*
        private T ProcessResponse<T>(IRestResponse response)
        {
            OnIncomingDataEvent(response);
            try
            {
                return Deserialize<T>(response.Content);
            }
            catch (GPClientException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new GPClientException($"Could not read server response. HTTP Stats code: \"{response.StatusCode}\", Content: \"{response.Content}\", {response.ErrorMessage}");
            }
        }
        */


        private T HandleResponse<T>(RestResponse<T> response)
        {
            try
            {                
                if (response.IsSuccessful)
                {
                    return Deserialize<T>(response.Content);
                }
                else
                {
                    HandleErrorResponse(response.Content);
                    return default;
                }
            }
            catch (Exception ex)
            {
                throw new GPClientException(ex.Message);
            }
        }

        /// <exception cref="GPClientException"></exception>
        private void HandleErrorResponse(string content)
        {
            if (content != null)
            {
                try
                {
                    throw new GPClientException(Deserialize<APIError>(content));
                } catch (DeserializationException)
                {
                    throw new GPClientException(content);
                }
                
            }
            throw new GPClientException("An Error occured"); 
            
            
        }

        private T Deserialize<T>(string Content)
        {
            return JsonConvert.DeserializeObject<T>(Content);
        }
        /*
        private T processComplex<T>(IRestResponse response)
        {
            try
            {
                return DeserializeComplex<T>(response.Content);
            }
            catch (Exception)
            {
                throw new GPClientException($"Could not read server response. HTTP Stats code: \"{response.StatusCode}\", Content: \"{response.Content}\", {response.ErrorMessage}");
            }
        }

        private T DeserializeComplex<T>(string Content)
        {
            if (Content.Contains("error_code"))
            {
                Deserialize<APIError>(Content);
            }
            return JsonConvert.DeserializeObject<T>(Content);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string ProcessPaymentInstrumentRootContent(string content)
        {
            string[] separator = new string[1];
            separator[0] = "enabledPaymentInstruments";
            string[] both = content.Split(separator, StringSplitOptions.None);
            both[0] = both[0].Replace("card-payment", "card_payment");
            both[0] = both[0].Replace("bank-transfer", "bank_transfer");
            content = both[0] + "enabledPaymentInstruments" + both[1];
            return content;
        }

        private string serializeToJson(object o)
        {
            return JsonConvert.SerializeObject(o, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

        }
        */
    }

}
