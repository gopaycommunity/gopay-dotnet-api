using GoPay.Payments;
using GoPay.Common;
using GoPay.Account;
using GoPay.EETProp;
using GoPay.Supercash;
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
using RestSharp.Serializers.NewtonsoftJson;

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

        public GPConnector(string APIUrl, string clientid, string clientsecret, bool setSecurityProtocol = true)
        {
            if (setSecurityProtocol)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }
            Client.BaseUrl = new Uri(APIUrl);
            ClientID = clientid;
            ClientSecret = clientsecret;
            Client.UserAgent = "GoPay .NET Client ";
            Client.UseNewtonsoftJson();
        }

        /// <exception cref="GPClientException"></exception>
        public GPConnector GetAppToken()
        {
            return GetAppToken(OAuth.SCOPE_PAYMENT_ALL);
        }

        /// <exception cref="GPClientException"></exception>
        public GPConnector GetAppToken(string scope)
        {
            var restRequest = new RestRequest(@"/oauth2/token", Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&scope=" + scope, ParameterType.RequestBody);
            var authenticator = new HttpBasicAuthenticator(ClientID, ClientSecret);
            authenticator.Authenticate(Client, restRequest);
            var response = Client.Execute(restRequest);
            OnIncomingDataEvent(response);
            AccessToken = ProcessResponse<AccessToken>(response);
            return this;
        }


        /// <exception cref="GPClientException"></exception>
        public Payment CreatePayment(BasePayment payment)
        {
            var restRequest = CreateRestRequest(@"/payments/payment", "application/json");

            var jsonData = serializeToJson(payment);
            restRequest.AddParameter("application/json", jsonData, ParameterType.RequestBody);

            var response = Client.Execute(restRequest);

            return ProcessResponse<Payment>(response);
        }

        public async Task<Payment> CreatePaymentAsync(BasePayment payment)
        {
            var restRequest = CreateRestRequest(@"/payments/payment", "application/json");

            var jsonData = serializeToJson(payment);
            restRequest.AddParameter("application/json", jsonData, ParameterType.RequestBody);

            var response = await Client.ExecuteAsync(restRequest);
            return await Task.Factory.StartNew(() => ProcessResponse<Payment>(response));
        }

        /// <exception cref="GPClientException"></exception>
        public PaymentResult RefundPayment(long id, long amount)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/refund", "multipart/form-data");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            restRequest.AddParameter("amount", amount);
            var response = Client.Execute<PaymentResult>(restRequest);
            return ProcessResponse<PaymentResult>(response);
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> RefundPaymentAsync(long id, long amount)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/refund", "multipart/form-data");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            restRequest.AddParameter("amount", amount);
            var response = await Client.ExecuteAsync(restRequest);

            var result = await Task.Factory.StartNew(() => ProcessResponse<PaymentResult>(response));
            return result;
        }

        /// <exception cref="GPClientException"></exception>
        public PaymentResult RefundPayment(long id, RefundPayment refundPayment)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/refund", "application/json");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);


            var jsonData = serializeToJson(refundPayment);
            restRequest.AddParameter("application/json", jsonData, ParameterType.RequestBody);

            var response = Client.Execute<PaymentResult>(restRequest);
            return ProcessResponse<PaymentResult>(response);
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> RefundPaymentAsync(long id, RefundPayment refundPayment)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/refund", "application/json");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);

            var jsonData = serializeToJson(refundPayment);
            restRequest.AddParameter("application/json", jsonData, ParameterType.RequestBody);

            var response = await Client.ExecuteAsync(restRequest);
            var result = await Task.Factory.StartNew(() => ProcessResponse<PaymentResult>(response));
            return result;
        }

        /// <exception cref="GPClientException"></exception>
        public Payment CreateRecurrentPayment(long id, NextPayment nextPayment)
        {
            var recurrentPayment = CreateRestRequest(@"/payments/payment/{id}/create-recurrence", "application/json");
            recurrentPayment.AddParameter("id", id, ParameterType.UrlSegment);

            var jsonData = serializeToJson(nextPayment);
            recurrentPayment.AddParameter("application/json", jsonData, ParameterType.RequestBody);

            var response = Client.Execute(recurrentPayment);
            return ProcessResponse<Payment>(response);
        }

        public async Task<Payment> CreateRecurrentPaymentAsync(long id, NextPayment nextPayment)
        {
            var recurrentPayment = CreateRestRequest(@"/payments/payment/{id}/create-recurrence", "application/json");
            recurrentPayment.AddParameter("id", id, ParameterType.UrlSegment);

            var jsonData = serializeToJson(nextPayment);
            recurrentPayment.AddParameter("application/json", jsonData, ParameterType.RequestBody);

            var content = await Client.ExecuteAsync(recurrentPayment);
            var result = await Task.Factory.StartNew(() => ProcessResponse<Payment>(content));
            return result;
        }

        /// <exception cref="GPClientException"></exception>
        public PaymentResult VoidRecurrency(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/void-recurrence", "application/x-www-form-urlencoded");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<PaymentResult>(response);
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> VoidRecurrencyAsync(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/void-recurrence", "application/x-www-form-urlencoded");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = await Client.ExecuteAsync(restRequest);
            var result = await Task.Factory.StartNew(() => ProcessResponse<PaymentResult>(response));
            return result;
        }

        /// <exception cref="GPClientException"></exception>
        public PaymentResult CapturePayment(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/capture", "application/x-www-form-urlencoded");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<PaymentResult>(response);
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> CapturePaymentAsync(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/capture", "application/x-www-form-urlencoded");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = await Client.ExecuteAsync(restRequest);
            return await Task.Factory.StartNew(() => ProcessResponse<PaymentResult>(response));
        }

        /// <exception cref="GPClientException"></exception>
        public PaymentResult CapturePayment(long id, CapturePayment capturePayment)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/capture", "application/json");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);

            var jsonData = serializeToJson(capturePayment);
            restRequest.AddParameter("application/json", jsonData, ParameterType.RequestBody);

            var response = Client.Execute(restRequest);
            return ProcessResponse<PaymentResult>(response);
        }

        /// <exception cref="GPClientException"></exception>
        public PaymentResult VoidAuthorization(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/void-authorization", "application/x-www-form-urlencoded");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<PaymentResult>(response);
        }

        /// <exception cref="GPClientException"></exception>
        public async Task<PaymentResult> VoidAuthorizationAsync(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/void-authorization", "application/x-www-form-urlencoded");
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = await Client.ExecuteAsync(restRequest);
            return await Task.Factory.StartNew(() => ProcessResponse<PaymentResult>(response));
        }

        /// <exception cref="ApplicationException"></exception>
        public Payment PaymentStatus(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}", "application/x-www-form-urlencoded", null, Method.GET);
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<Payment>(response);
        }

        /// <exception cref="ApplicationException"></exception>
        public async Task<Payment> PaymentStatusAsync(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}", "application/x-www-form-urlencoded", null, Method.GET);
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = await Client.ExecuteAsync(restRequest);
            return await Task.Factory.StartNew(() => ProcessResponse<Payment>(response));
        }

        /// <exception cref="GPClientException"></exception>
        public PaymentInstrumentRoot GetPaymentInstruments(long goid, Currency currency)
        {
            var restRequest = CreateRestRequest(@"/eshops/eshop/{goid}/payment-instruments/{currency}", null, null, Method.GET);
            restRequest.AddParameter("goid", goid, ParameterType.UrlSegment);
            restRequest.AddParameter("currency", currency, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);

            if (!response.Content.Contains("error_code"))
            {
                response.Content = ProcessPaymentInstrumentRootContent(response.Content);
            }

            return ProcessResponse<PaymentInstrumentRoot>(response);
        }

        /// <exception cref="GPClientException"></exception>
        public byte[] GetStatement(AccountStatement accountStatement)
        {
            var restRequest = CreateRestRequest(@"/accounts/account-statement", "application/json");
            var jsonData = serializeToJson(accountStatement);

            restRequest.AddParameter("application/json", jsonData, ParameterType.RequestBody);
            var response = Client.Execute(restRequest);
            string content = response.Content.ToString();

            if (content.Contains("error_code"))
            {
                ProcessResponse<APIError>(response);
            }

            return System.Text.Encoding.UTF8.GetBytes(content);
        }

        /// <exception cref="GPClientException"></exception>
        public List<EETReceipt> FindEETReceiptsByFilter(EETReceiptFilter filter)
        {
            var restRequest = CreateRestRequest(@"/eet-receipts", "application/json");

            var jsonData = serializeToJson(filter);
            restRequest.AddParameter("application/json", jsonData, ParameterType.RequestBody);

            var response = Client.Execute(restRequest);
            return processComplex<List<EETReceipt>>(response);

        }

        /// <exception cref="ApplicationException"></exception>
        public List<EETReceipt> GetEETReceiptByPaymentId(long id)
        {
            var restRequest = CreateRestRequest(@"/payments/payment/{id}/eet-receipts", "application/json", null, Method.GET);
            restRequest.AddParameter("id", id, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);
            return processComplex<List<EETReceipt>>(response);
        }

        [Obsolete("Payment method SuperCash is no longer supported", false)]
        /// <exception cref="GPClientException"></exception>
        public SupercashCoupon CreateSupercashCoupon(SupercashCouponRequest couponRequest)
        {
            var restRequest = CreateRestRequest(@"/supercash/coupon", "application/json");

            var jsonData = serializeToJson(couponRequest);
            restRequest.AddParameter("application/json", jsonData, ParameterType.RequestBody);

            var response = Client.Execute(restRequest);
            return ProcessResponse<SupercashCoupon>(response);
        }

        [Obsolete("Payment method SuperCash is no longer supported", false)]
        /// <exception cref="GPClientException"></exception>
        public SupercashBatchResult CreateSupercashCouponBatch(SupercashBatchRequest batchRequest)
        {
            var restRequest = CreateRestRequest(@"/supercash/coupon/batch", "application/json");

            var jsonData = serializeToJson(batchRequest);
            restRequest.AddParameter("application/json", jsonData, ParameterType.RequestBody);
            var response = Client.Execute(restRequest);
            return ProcessResponse<SupercashBatchResult>(response);
        }

        /// <exception cref="ApplicationException"></exception>
        public SupercashBatchState GetSupercashCouponBatchStatus(long batchId)
        {
            var restRequest = CreateRestRequest(@"/batch/{batch_id}", "application/x-www-form-urlencoded", null, Method.GET);
            restRequest.AddParameter("batch_id", batchId, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<SupercashBatchState>(response);
        }

        public SupercashBatch GetSupercashCouponBatch(long batchId, long goid)
        {
            var restRequest = CreateRestRequest(@"/supercash/coupon/find?batch_request_id={batch_id}&go_id={go_id}", "application/x-www-form-urlencoded", null, Method.GET);
            restRequest.AddParameter("batch_id", batchId, ParameterType.UrlSegment);
            restRequest.AddParameter("go_id", goid, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<SupercashBatch>(response);
        }

        public SupercashBatch FindSupercashCoupons(long goid, params long[] paymentSessionIds)
        {
            string ids = string.Join(",", paymentSessionIds);
            var restRequest = CreateRestRequest(@"/supercash/coupon/find?payment_session_id_list=" + ids + "&go_id={go_id}", "application/x-www-form-urlencoded", null, Method.GET);
            restRequest.AddParameter("go_id", goid, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);
            return ProcessResponse<SupercashBatch>(response);
        }

        public SupercashPayment GetSupercashCoupon(long couponId)
        {
            var restRequest = CreateRestRequest(@"/supercash/coupon/{coupon_id}", "application/x-www-form-urlencoded", null, Method.GET);
            restRequest.AddParameter("coupon_id", couponId, ParameterType.UrlSegment);
            var response = Client.Execute(restRequest);

            return ProcessResponse<SupercashPayment>(response);
        }

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

        private T Deserialize<T>(string Content)
        {
            var err = JsonConvert.DeserializeObject<APIError>(Content);
            if (err.ErrorMessages != null)
            {
                var ex = new GPClientException();
                ex.Error = err;
                throw ex;
            }
            return JsonConvert.DeserializeObject<T>(Content);
        }

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


        private IRestRequest CreateRestRequest(string url, string contentType)
        {
            return CreateRestRequest(url, contentType, null);
        }

        private IRestRequest CreateRestRequest(string url, string contentType, Parameter parameter, Method method = Method.POST)
        {

            var restRequest = new RestRequest(url, method);
            if (parameter != null)
            {
                restRequest.AddParameter(parameter);
            }
            restRequest.AddHeader("Accept", "application/json");

            if (contentType != null)
            {
                restRequest.AddHeader("Content-Type", contentType);
            }

            restRequest.AddHeader("Authorization", "Bearer " + AccessToken.Token);
            return restRequest;
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

        private void OnIncomingDataEvent(IRestResponse response)
        {
            IncomingDataEventHandler.Invoke(this, ResponseToSHD(response));
        }

        public delegate void ServerEventHandler(object sender, ServerHandlerData myValue);

        public event ServerEventHandler IncomingDataEventHandler = delegate { };

        public struct ServerHandlerData
        {
            public long ContentLength { get; internal set; }
            public HttpStatusCode HttpStatusCode { get; internal set; }
            public byte[] Body { get; internal set; }
            public string StatusDescription { get; internal set; }
            public string ContentEncoding { get; internal set; }
            public string ContentType { get; internal set; }
        }

        public ServerHandlerData ResponseToSHD(IRestResponse response)
        {
            return new ServerHandlerData
            {
                HttpStatusCode = response.StatusCode,
                Body = (byte[])(response.RawBytes == null ? null : response.RawBytes.Clone()),
                StatusDescription = response.StatusDescription,
                ContentEncoding = response.ContentEncoding,
                ContentType = response.ContentType
            };
        }
    }
}
