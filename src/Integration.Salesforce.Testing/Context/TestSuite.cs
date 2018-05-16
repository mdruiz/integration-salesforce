using System.Collections.Generic;
using Integration.Salesforce.Context;
using Integration.Salesforce.Library.Models;
using Microsoft.Extensions.Options;
using Xunit;

namespace Integration.Salesforce.Testing.Context
{
  public class TestSuite
  {
        public void TestPersonEntry()
        {
            Settings settings = new Settings();

            settings.ConnectionString = DbOptionsFactory.ConnectionString;
            settings.Database = DbOptionsFactory.DatabaseName;

            DbContext<Person> context = new DbContext<Person>(settings);

            //insert preset person models
            IEnumerable<Person> models = GetPersonTestModels();
            context.UpdateMongoDB(models);
            
            List<Person> pl = (List<Person>) context.ReadMongoEntries();
            bool r = false;
            foreach(var item in pl)
            {
                if(item.FirstName.Equals("james"))
                {
                    r = true;
                }
            }
            context.DeleteMongoEntries(models);
            Assert.True(r);
        }
        public void DeleteAllMongoEntries()
        {
            Settings settings = new Settings();

            settings.ConnectionString = DbOptionsFactory.ConnectionString;
            settings.Database = DbOptionsFactory.DatabaseName;

            DbContext<Person> context = new DbContext<Person>(settings);

            context.DeleteMongoEntries(context.ReadMongoEntries());

            var r = true;
            foreach(var item in context.ReadMongoEntries())
            {
                if(item.FirstName == null)
                {
                    r = false;
                }
            }
            Assert.True(r);
        }
        public IEnumerable<Person> GetPersonTestModels()
        {
            Person person1 = new Person();
            person1.Active = true;
            Person person2 = new Person();
            person2.Active = true;
            
            
            person1.FirstName = "james";
            person2.FirstName = "jim";

            List<Person> modelList = new List<Person>();            

            modelList.Add(person1);
            modelList.Add(person2);

            return modelList;
        }

        public void RemoveMongoDB()
        {
            Settings settings = new Settings();

            settings.ConnectionString = DbOptionsFactory.ConnectionString;
            settings.Database = DbOptionsFactory.DatabaseName;

            DbContext<Person> context = new DbContext<Person>(settings);

            context.RemoveAllMongoEntries();

        }
        
    }
}
