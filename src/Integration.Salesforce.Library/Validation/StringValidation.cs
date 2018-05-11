using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Integration.Salesforce.Library.Validation
{
    [AttributeUsage(AttributeTargets.Property | 
        AttributeTargets.Field, AllowMultiple = false)]
    sealed public class StringValidation : ValidationAttribute
    {
        public bool IsValid(string value)
        {
        if(!(value.Any(char.IsDigit)))
        {
            return true;
        }
        else
        {
            return false;
        }
        }
        public override string FormatErrorMessage(string name)
        {
        return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }
    }
}