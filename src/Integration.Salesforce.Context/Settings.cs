using System.Collections.Generic;

namespace Integration.Salesforce.Context
{
    /// <summary>
    /// This class is used to get connectionstring name, database name, and collection name from appSettings.json file for MongoDB.
    /// Then, we can inject this to any dbcontext with IOptions
    /// </summary>
    public class Settings
    {
        //connectionstring name
        public string ConnectionString { get; set; }
        //database name
        public string Database { get; set; }
        //collection name
        public string CollectionName { get; set; }
        public int CacheExpirationMinutes { get; set; }

        // Salesforce request body information
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Salesforce URLs
        public string LoginUrl { get; set; }
        public string ResourceUrlExtension { get; set; }

        public Settings()
        {
            
        }

        public Settings(List<string> Strings)
        {
            ConnectionString = Strings[0];
            Database = Strings[1];
            ClientId = Strings[2];
            ClientSecret = Strings[3];
            Username = Strings[4];
            Password = Strings[5];
            LoginUrl = Strings[6];
            ResourceUrlExtension = Strings[7];
        }
    }
}