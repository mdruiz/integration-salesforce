using System;
using Integration.Salesforce.Library.Models;
using Newtonsoft.Json.Linq;
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
    public class MappingTesting
  {
    //sample contact
    static JObject o = JObject.FromObject(new
      {
        FirstName = "Test",
        LastName = "Testerson",
        MobilePhone = "555555",
        Candidate_Type__c = "TestRole",
        HR_Has_Car__c = "True",
        Email = "test@test.com",
        Training_Batch__c = "Test Batch",
        Housing_Agreement__c = "true",
        MailingStreet = "7357 st",
        MailingCity = "Test City",
        MailingState = "TS",
        MailingPostalCode = "7357146",
        MailingCountry = "TestCountry"
      }
    );
    //test mapping is correct for address and person
    //TODO: test what happens when json isn't formatted properly
    //TODO: test when json doesn't exist
    
    [Fact]
    public void TestAddressMap()
    {
      Address add = new Address();
      add.MapJsonToModel(o);
      Assert.Equal(add.StreetAddress, o["MailingStreet"].ToString());
      Assert.Equal(add.City, o["MailingCity"].ToString());
      Assert.Equal(add.State, o["MailingState"]);
      Assert.Equal(add.Zip, o["MailingPostalCode"]);
      Assert.Equal(add.Country, o["MailingCountry"].ToString());
    }
    [Fact]
    public void TestPersonMap()
    {
      Person p = new Person();
      p.MapJsonToModel(o);
      Assert.Equal(p.FirstName, o["FirstName"].ToString());
      Assert.Equal(p.LastName, o["LastName"].ToString());
      Assert.Equal(p.Phone, o["MobilePhone"].ToString());
      Assert.Equal(p.Role, o["Candidate_Type__c"].ToString());
      Assert.Equal(p.HasCar, o["HR_Has_Car__c"].ToObject<bool>());
      Assert.Equal(p.EMail, o["Email"].ToString());
      Assert.Equal(p.Batch, o["Training_Batch__c"].ToString());
      Assert.Equal(p.HousingStatus, o["Housing_Agreement__c"].ToObject<bool>());
    }
  }
}