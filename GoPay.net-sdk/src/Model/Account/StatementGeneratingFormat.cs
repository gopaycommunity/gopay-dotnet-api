using System;
using System.ComponentModel;
using System.Reflection;

namespace GoPay.Account
{
    public enum StatementGeneratingFormat
    {

        [ContentType("application/vnd.ms-excel")]XLS_A,
        [ContentType("application/vnd.ms-excel")]XLS_B,
        [ContentType("application/vnd.ms-excel")]XLS_C,
        [ContentType("text/csv")]CSV_A,
        [ContentType("text/csv")]CSV_B,
        [ContentType("text/csv")]CSV_C,
        [ContentType("text/csv")]CSV_D,
        [ContentType("application/abo")]ABO_A

    }

    public static class StatementGeneratingFormatExtension
    {

        public static string GetType(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = (ContentType)fieldInfo.GetCustomAttribute(typeof(ContentType));
            return attribute.Type;
        }
    }

    public class ContentType : Attribute
    {
        public string Type { get; set; }

        public ContentType(string type) : base()
        {
            Type = type;
        }
    }
}

