using Xunit;

namespace Integration.Salesforce.Testing.Context
{
  public class TestSuite
  {
    public void DatabaseConnection()
        {
            IOptions<Settings> settings = Options.Create(new Settings());
             string connectionString = 
                "mongodb://integration-dev1:HsVcVG5kDlyYiwX26ilme1WA3wtwovx4Xqve8sQapHzRAZD0bghwcADLLzSZb5HTG1BWvNMCiVWlkEykT2O0LQ==@integration-dev1.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
            settings.Value.ConnectionString = connectionString;
            settings.Value.Database = "housingdb";
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
             string connectionString = 
                "mongodb://integration-dev1:HsVcVG5kDlyYiwX26ilme1WA3wtwovx4Xqve8sQapHzRAZD0bghwcADLLzSZb5HTG1BWvNMCiVWlkEykT2O0LQ==@integration-dev1.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
            settings.Value.ConnectionString = connectionString;
            settings.Value.Database = "housingdb";
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
