using System;
using System.Reflection;
using GoPay.Model.Payments;

namespace GoPay.Common
{
    public enum CheckoutGroup
    {
        
        [GroupCaption("card-payment"), EnumSetPaymentInstruments(PaymentInstrument.PAYMENT_CARD)]
        CARD_PAYMENT,

        [GroupCaption("bank-transfer"), EnumSetPaymentInstruments(PaymentInstrument.BANK_ACCOUNT)]
        BANK_TRANSFER,

        [GroupCaption("wallet"), EnumSetPaymentInstruments(PaymentInstrument.GOPAY, PaymentInstrument.BITCOIN, PaymentInstrument.PAYPAL)]
        WALLET,

        [GroupCaption("others"), EnumSetPaymentInstruments(PaymentInstrument.PRSMS, PaymentInstrument.MPAYMENT, PaymentInstrument.PAYSAFECARD, PaymentInstrument.SUPERCASH, PaymentInstrument.GPAY)]
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
