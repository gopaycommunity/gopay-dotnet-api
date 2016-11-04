﻿using System;
using System.ComponentModel;
using System.Reflection;

namespace GoPay.Common
{
    public enum Currency
    {

        [CurrencyCode(203)]CZK,
        [CurrencyCode(985)]PLN,
        [CurrencyCode(978)]EUR
            
    }

    public static class CurrencyExtension
    {
        
        public static int GetCode(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = (CurrencyCode)fieldInfo.GetCustomAttribute(typeof(CurrencyCode));
            return attribute.Code;
        }
    }

    public class CurrencyCode : Attribute
    {
        public int Code { get; set; }

        public CurrencyCode(int code) : base()
        {
            Code = code;
        }
    }
     
}
