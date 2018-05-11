using System;
using Integration.Salesforce.Library.Models;
using Xunit;

namespace Integration.Salesforce.Testing.Library.ModelTests
{
    public class TestSuite
    {
        ModelData md = new ModelData();

        [Fact]
        public void ContactToString_ReturnsValidString_True()
        {
            // assemble
            Person person = md.Person();
            Address address = md.Address();
            // act
            var response = person.ToString();
            var teststring = $"PERSON{{Name:{person.FirstName} {person.LastName};Phone:{person.Phone};EMail:{person.EMail};Role:{person.Role};HasCar:{person.HasCar};BatchName:{person.BatchName};}}";
            teststring += address.ToString();
            // assert
            Assert.NotNull(response);
            Assert.Equal(teststring, response, true);
        }

        [Fact]
        public void AddressToString_ReturnsValidString_True()
        {
            // assemble
            Address address = md.Address();
            // act
            var response = address.ToString();
            var teststring = $"ADDRESS{{StreetAddress:{address.StreetAddress};City:{address.City};State:{address.State};Zip:{address.Zip};}}";
            // assert
            Assert.NotNull(response);
            Assert.Equal(teststring, response, true);
        }
    }
}