using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Integration.Salesforce.Library.Validation
{
    [AttributeUsage(AttributeTargets.Property |
        AttributeTargets.Field, AllowMultiple = false)]
    sealed public class NumberValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
        switch (Type.GetTypeCode(value.GetType()))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
            return true;
            default:
            return false;
        }
        }

        public override string FormatErrorMessage(string name)
        {
        return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }
    }
}
