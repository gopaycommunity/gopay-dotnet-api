using System;
using System.Reflection;
using GoPay.Model.Payments;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace GoPay.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CheckoutGroup
    {
        
        [GroupCaption("card-payment"), EnumSetPaymentInstruments(PaymentInstrument.PAYMENT_CARD)]
        [EnumMember(Value = "card-payment")]
        CARD_PAYMENT,

        [GroupCaption("bank-transfer"), EnumSetPaymentInstruments(PaymentInstrument.BANK_ACCOUNT)]
        [EnumMember(Value = "bank-transfer")]
        BANK_TRANSFER,

        [GroupCaption("wallet"), EnumSetPaymentInstruments(PaymentInstrument.GOPAY, PaymentInstrument.BITCOIN, PaymentInstrument.PAYPAL)]
        [EnumMember(Value = "wallet")]
        WALLET,

        [GroupCaption("others"), EnumSetPaymentInstruments(PaymentInstrument.PRSMS, PaymentInstrument.MPAYMENT, PaymentInstrument.PAYSAFECARD, PaymentInstrument.SUPERCASH)]
        [EnumMember(Value = "others")]
        OTHERS

    }


    public static class CheckoutGroupExtension
    {

        public static string GetCaption(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = (GroupCaption)fieldInfo.GetCustomAttribute(typeof(GroupCaption));
            return attribute.Caption;
        }


        public static PaymentInstrument[] GetEnumSetPaymentInstruments(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = (EnumSetPaymentInstruments)fieldInfo.GetCustomAttribute(typeof(EnumSetPaymentInstruments));
            return attribute.EnumSetInstruments;
        }
    }


    public class GroupCaption : Attribute
    {
        public string Caption { get; set; }

        public GroupCaption(string caption) : base()
        {
            Caption = caption;
        }
    }


    public class EnumSetPaymentInstruments : Attribute
    {
        public PaymentInstrument[] EnumSetInstruments { get; set; }

        public EnumSetPaymentInstruments(params PaymentInstrument[] instruments) : base()
        {
            EnumSetInstruments = instruments;
        }
    }

}
