using System.ComponentModel.DataAnnotations;
using Integration.Salesforce.Library.Abstract;
using Integration.Salesforce.Library.Validation;
using Newtonsoft.Json.Linq;

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
      public string State { get; set; }

      [Required]
      [NumberValidation(ErrorMessage = "{0} invalid number input")]
      public override int Zip { get; set; }
      [Required]
      [StringValidation(ErrorMessage = "{0} invalid string input")]
      public string Country {get;set;}

      public void MapJsonToModel(JObject json)
      {
        //Address (Street, city, state, postal code, country)
        this.StreetAddress = json["MailingStreet"].ToString();
        this.City = json["MailingCity"].ToString();
        this.State = json["MailingState"].ToString();
        this.Zip = json["MailingPostalCode"].ToObject<int>();
        this.Country = json["MailingCountry"].ToString();
      }

      public override string ToString()
      {
        return $"{StreetAddress}, {City}, {State}, {Zip}";
      }
    }
}