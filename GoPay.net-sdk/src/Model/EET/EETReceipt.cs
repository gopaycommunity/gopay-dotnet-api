using System;
using Newtonsoft.Json;
using GoPay.Common;
using Newtonsoft.Json.Converters;

namespace GoPay.EETProp
{
    public class EETReceipt
    {

        public enum EETReceiptState
        {
            CREATED,
            DELIVERY_FAILED,
            DELIVERED
        }

        public enum EETMode
        {
            AUTO,
            EET
        }

        [JsonProperty("payment_id")]
        public long PaymentId { get; set; }

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EETReceiptState State { get; set; }

        [JsonConverter(typeof(GPDateAdapter))]
        [JsonProperty("date_last_attempt")]
        public DateTime DateLastAttempt { get; set; }

        [JsonConverter(typeof(GPDateAdapter))]
        [JsonProperty("date_next_attempt")]
        public DateTime DateNextAttempt { get; set; }

        [JsonProperty("error_code")]
        public int CommErrorCode { get; set; }

        [JsonProperty("kod_chyby")]
        public int ErrorCode { get; set; }

        [JsonProperty("kody_varovani")]
        public string KodyVarovani { get; set; }

        [JsonProperty("eet_mode")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EETMode EetMode { get; set; }

        [JsonProperty("uuid_zprava")]
        public string UuidZprava { get; set; }

        [JsonConverter(typeof(GPDateAdapter))]
        [JsonProperty("date_odesl")]
        public DateTime DatOdesl { get; set; }

        [JsonProperty("dic_popl")]
        public string DicPopl { get; set; }

        [JsonProperty("id_provoz")]
        public int IdProvoz { get; set; }

        [JsonProperty("id_pokl")]
        public string IdPokl { get; set; }

        [JsonConverter(typeof(GPDateAdapter))]
        [JsonProperty("dat_trzby")]
        public DateTime DatTrzby { get; set; }

        [JsonProperty("porad_cis")]
        public string PoradCis { get; set; }

        [JsonProperty("fik")]
        public string Fik { get; set; }

        [JsonProperty("bkp")]
        public string Bkp { get; set; }

        [JsonProperty("pkp")]
        public string Pkp { get; set; }

        [JsonProperty("celk_trzba")]
        public long CelkTrzba { get; set; }

        [JsonProperty("zakl_nepodl_dph")]
        public long ZaklNepodlDPH { get; set; }

        [JsonProperty("zakl_dan1")]
        public long ZaklDan1 { get; set; }

        [JsonProperty("dan1")]
        public long Dan1 { get; set; }

        [JsonProperty("zakl_dan2")]
        public long ZaklDan2 { get; set; }

        [JsonProperty("dan2")]
        public long Dan2 { get; set; }

        [JsonProperty("zakl_dan3")]
        public long ZaklDan3 { get; set; }

        [JsonProperty("dan3")]
        public long Dan3 { get; set; }

        [JsonProperty("cest_sluz")]
        public long CestSluz { get; set; }

        [JsonProperty("pouzit_zboz1")]
        public long PouzitZboz1 { get; set; }

        [JsonProperty("pouzit_zboz2")]
        public long PouzitZboz2 { get; set; }

        [JsonProperty("pouzit_zboz3")]
        public long PouzitZboz3 { get; set; }

        [JsonProperty("urceno_cerp_zuct")]
        public long UrcenoCerpZuct { get; set; }

        [JsonProperty("cerp_zuct")]
        public long CerpZuct { get; set; }

        [JsonProperty("dic_poverujiciho")]
        public string DicPoverujiciho { get; set; }


        public override string ToString()
        {
            return string.Format(
                   "EETReceipt [paymentId={0}, state={1}, idProvoz={2}, datTrzby={3}, celkTrzba={4}]",
                   PaymentId, Enum.GetName(typeof(EETReceiptState), State), IdProvoz, DatTrzby, CelkTrzba
                   );
        }

    }
}
