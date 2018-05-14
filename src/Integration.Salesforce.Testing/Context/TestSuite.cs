using System.Collections.Generic;
using Integration.Salesforce.Context;
using Integration.Salesforce.Library.Models;
using Microsoft.Extensions.Options;
using Xunit;

namespace Integration.Salesforce.Testing.Context
{
  public class TestSuite
  {
      [Fact]
    public void DatabaseConnection()
        {
            IOptions<Settings> settings = Options.Create(new Settings());

            settings.Value.ConnectionString = DbOptionsFactory.ConnectionString;
            settings.Value.Database = DbOptionsFactory.DatabaseName;

            DbContext<Person> context = new DbContext<Person>(settings);

            //insert preset models
            context.UpdateMongoDB(context.GetModels());
            
            List<Person> pl = (List<Person>) context.ReadMongoEntries();
            bool r = false;
            foreach(var item in pl)
            {
                if(item.FirstName.Equals("james"))
                {
                    r = true;
                }
            }
            Assert.True(r);
        }
        [Fact]
        public void DeleteAllMongoEntries()
        {
            IOptions<Settings> settings = Options.Create(new Settings());

            settings.Value.ConnectionString = DbOptionsFactory.ConnectionString;
            settings.Value.Database = DbOptionsFactory.DatabaseName;

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
    }
}
