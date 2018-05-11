using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Integration.Salesforce.Library.Validation
{
    [AttributeUsage(AttributeTargets.Property |
        AttributeTargets.Field, AllowMultiple = false)]
    sealed public class DateValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
        DateTime temp;

        if(DateTime.TryParse(Convert.ToString(value), out temp) == true)
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
