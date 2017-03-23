using System;
using Newtonsoft.Json;
using GoPay.Common;
using Newtonsoft.Json.Converters;


namespace GoPay.EETProp
{
    public class EET
    {

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

        [JsonProperty("mena")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Mena { get; set; }

        public EET()
        {

        }

        public EET (long celkTrzba, Currency mena)
        {
            this.CelkTrzba = celkTrzba;
            this.Mena = mena;
        }

        public override string ToString()
        {
            return string.Format("EET [celkTrzba={0}, zaklNepodlDPH={1}, zaklDan1={2}, dan1={3}, zaklDan2={4}, dan2={5}, zaklDan3={6}, dan3={7}, "
                + "cestSluz={8}, pouzitZboz1={9}, pouzitZboz2={10}, pouzitZboz3={11}, urcenoCerpZuct={12}, cerpZuct={13}, dicPoverujiciho={14}, "
                + "mena={15}]",
                CelkTrzba, ZaklNepodlDPH, ZaklDan1, Dan1, ZaklDan2, Dan2, ZaklDan3, Dan3, CestSluz, PouzitZboz1, PouzitZboz2, PouzitZboz3, UrcenoCerpZuct, CerpZuct,
                DicPoverujiciho, Enum.GetName(typeof(Currency), Mena)
                );
        }
    }
}

