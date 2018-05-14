using System;
using System.ComponentModel.DataAnnotations;
using Integration.Salesforce.Library.Abstract;
using Integration.Salesforce.Library.Validation;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;

namespace Integration.Salesforce.Library.Models
{
    public class Person : AModel
    {
        public Person()
        {
            ModelType = "persons";
            Address = new Address();
        }

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

        public bool HousingStatus { get; set; }

        public override string ToString()
        {
            string returnString = $"PERSON{{Name:{FirstName} {LastName};Phone:{Phone};Role:{Role};HasCar:{HasCar};BatchName:{BatchName};}}";
            returnString += Address.ToString();

            return returnString;
        }

        public override void MapJsonToModel(JObject json)
        {
            //Services need at least:
            //Name (First, last), Email, BatchName,IsMale, Phone number, HasCar, Address (Street, city, state, postal code, country)

            try
            {
                if (json == null)
                {
                    throw new ArgumentNullException();
                }
                this.FirstName = json.GetValue("FirstName").Value<string>();
                this.LastName = json.GetValue("LastName").Value<string>();
                this.Phone = json.GetValue("MobilePhone").Value<string>();
                this.Role = json.GetValue("Candidate_Type__c").Value<string>();
                this.HasCar = json.GetValue("HR_Has_Car__c").Value<bool>();
                this.Address.MapJsonToModel(json);
                this.EMail = json["Email"].ToString();
                this.BatchName = json["Training_Batch__c"].ToString();
                this.HousingStatus = json["Housing_Agreement__c"].ToObject<bool>();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("No json");
                throw e;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Attribute not found");
                throw e;
            }
            catch (Exception e)
            {
                Console.WriteLine("Mapping Failed");
                throw e;
            }
        }
    }
}
// namespace Integration.Salesforce.Library.Models
// {
//     public class Person
//     {
//         [Required]
//         [StringValidation(ErrorMessage = "{0} invalid string input")]
//         public string FirstName { get; set; }

//         [Required]
//         [StringValidation(ErrorMessage = "{0} invalid string input")]
//         public string LastName { get; set; }

//         [Required]
//         [PhoneValidation(ErrorMessage = "{0} invalid phone number input")]
//         public string Phone { get; set; }

//         [Required]
//         [StringValidation(ErrorMessage = "{0} invalid string input")]
//         public string Role { get; set; }

//         [Required]
//         [BoolValidation(ErrorMessage = "{0} invalid bool input")]
//         public bool HasCar { get; set; }

//         [Required]
//         public Address Address { get; set; }

//         [Required]
//         public string EMail { get; set; }

//         [Required]
//         public string Batch { get; set; }
//         public bool HousingStatus { get; private set; }

//         public Person()
//         {
//             Address = new Address();
//         }
//         public override string ToString()
//         {
//             return $"{FirstName} {LastName}, {Phone}, {Role}, {HasCar}, {Address}, {EMail}, {Batch}";
//         }

//         public void MapJsonToModel(JObject json)
//         {
//             //Services need at least:
//             //Name (First, last), Email, BatchName,IsMale, Phone number, HasCar, Address (Street, city, state, postal code, country)

//             try
//             {
//                 if (json == null)
//                 {
//                     throw new ArgumentNullException();
//                 }
//                 this.FirstName = json.GetValue("FirstName").Value<string>();
//                 this.LastName = json.GetValue("LastName").Value<string>();
//                 this.Phone = json.GetValue("MobilePhone").Value<string>();
//                 this.Role = json.GetValue("Candidate_Type__c").Value<string>();
//                 this.HasCar = json.GetValue("HR_Has_Car__c").Value<bool>();
//                 this.Address.MapJsonToModel(json);
//                 this.EMail = json["Email"].ToString();
//                 this.Batch = json["Training_Batch__c"].ToString();
//                 this.HousingStatus = json["Housing_Agreement__c"].ToObject<bool>();
//             }
//             catch (ArgumentNullException e)
//             {
//                 Console.WriteLine("No json");
//                 throw e;
//             }
//             catch (NullReferenceException e)
//             {
//                 Console.WriteLine("Attribute not found");
//                 throw e;
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine("Mapping Failed");
//                 throw e;
//             }
//         }
//     }
// }