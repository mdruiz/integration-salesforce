using System;
using System.Collections.Generic;
using Integration.Salesforce.Library.Models;

namespace Integration.Salesforce.Testing.Library.ModelTests
{
    public class ModelData
    {public Person Person()
        {
            Person person = new Person();
            person.FirstName = "Fred";
            person.LastName = "Belotte";
            person.Phone = "1231231234";
            person.Role = "Trainer";
            person.Address = Address();
            person.EMail = "fakeemail@fakeemail.com";

            return person;
        }

        public Address Address()
        {
            Address address = new Address();
            address.StreetAddress = "123 S 1st st";
            address.City = "Tampa";
            address.State = "FL";
            address.Zip = 33617;

            return address;
        }
    }
}