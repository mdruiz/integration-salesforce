using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Integration.Salesforce.Library.Validation
{
    [AttributeUsage(AttributeTargets.Property |
        AttributeTargets.Field, AllowMultiple = false)]
    sealed public class BoolValidation : ValidationAttribute
    {
        public bool IsValid(bool value)
        {
        if (value == true)
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