using System;
using Integration.Salesforce.Library.Models;
using Xunit;

namespace Integration.Salesforce.Testing.Library.ModelTests
{
    public class TestSuite
    {
        [Theory]
        [InlineData("derek", "white", "1231231234", "email@email.com", "trainer", true, "1803-mar12")]
        public void ContactToString_ReturnsValidString_True(string fn, string ln, string ph, string em, string ro, bool hc, string ba)
        {
            // assemble
            Person person = new Person();
            Address address = new Address();
            address.StreetAddress = "street address";
            address.City = "city";
            address.State = "st";
            address.Zip = 94928;

            person.FirstName = fn;
            person.LastName = ln;
            person.Phone = ph;
            person.EMail = em;
            person.Role = ro;
            person.HasCar = hc;
            person.BatchName = ba;
            person.Address = address;
            // act
            var response = person.ToString();
            var teststring = $"PERSON{{Name:{person.FirstName} {person.LastName};Phone:{person.Phone};EMail:{person.EMail};Role:{person.Role};HasCar:{person.HasCar};BatchName:{person.BatchName};}}";
            teststring += address.ToString();
            // assert
            Assert.NotNull(response);
            Assert.Equal(teststring, response, true);
        }

        [Theory]
        [InlineData("street address", "city", "st", 94928)]
        public void AddressToString_ReturnsValidString_True(string sa, string ci, string st, int zi)
        {
            // assemble
            Address address = new Address();
            address.StreetAddress = sa;
            address.City = ci;
            address.State = st;
            address.Zip = zi;
            // act
            var response = address.ToString();
            var teststring = $"ADDRESS{{StreetAddress:{address.StreetAddress};City:{address.City};State:{address.State};Zip:{address.Zip};}}";
            // assert
            Assert.NotNull(response);
            Assert.Equal(teststring, response, true);
        }
    }
}