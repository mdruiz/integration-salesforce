using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Integration.Salesforce.Library.Validation
{
    [AttributeUsage(AttributeTargets.Property |
        AttributeTargets.Field, AllowMultiple = false)]
    public class PhoneValidation : ValidationAttribute
    {
        public bool IsValid(string number)
        {
        if (Regex.Match(number, @"^([0-9]{10})$").Success)
        {
            return true;
        }
        return false;
        }

        public override string FormatErrorMessage(string name)
        {
        return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }
    }
}