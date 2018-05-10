using System.ComponentModel.DataAnnotations;
using Integration.Salesforce.Library.Abstract;
using Integration.Salesforce.Library.Validation;

namespace Integration.Salesforce.Library.Models
{
    public class Address : AbstractAddress
    {
        [Required]
        [StringValidation(ErrorMessage = "{0} invalid string input")]
        public override string StreetAddress { get; set; }

        [Required]
        [StringValidation(ErrorMessage = "{0} invalid string input")]
        public override string City { get; set; }

        [Required]
        [MaxLength(2)]
        [StringValidation(ErrorMessage = "{0} invalid string input")]
        public override string State { get; set; }

        [Required]
        [MaxLength(5)]
        [NumberValidation(ErrorMessage = "{0} invalid number input")]
        public override int Zip { get; set; }

        public override string ToString()
        {
            string returnString = $"ADDRESS{{StreetAddress:{StreetAddress};City:{City};State:{State};Zip:{Zip};}}";

            return returnString;
        }
    }
}
