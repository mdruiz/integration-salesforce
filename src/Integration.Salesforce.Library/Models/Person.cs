using System;
using System.ComponentModel.DataAnnotations;
using Integration.Salesforce.Library.Validation;
using MongoDB.Bson.Serialization.Attributes;

namespace Integration.Salesforce.Library.Models
{
    public class Person
    {
        [Required]
        [StringValidation(ErrorMessage = "{0} invalid string input")]
        public string FirstName { get; set; }

        [Required]
        [StringValidation(ErrorMessage = "{0} invalid string input")]
        public string LastName { get; set; }

        [Required]
        [PhoneValidation(ErrorMessage = "{0} invalid phone number input")]
        public string Phone { get; set; }

        [Required]
        [StringValidation(ErrorMessage = "{0} invalid string input")]
        public string Role { get; set; }

        [BoolValidation(ErrorMessage = "{0} invalid bool input")]
        public bool HasCar { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        [StringValidation(ErrorMessage = "{0} invalid string input")]
        public string EMail { get; set; }

        [Required]
        [StringValidation(ErrorMessage = "{0} invalid string input")]
        public string BatchName { get; set; }

        public override string ToString()
        {
            string returnString = $"PERSON{{Name:{FirstName} {LastName};Phone:{Phone};Role:{Role};HasCar:{HasCar};BatchName:{BatchName};}}";
            returnString += Address.ToString();

            return returnString;
        }
    }
}