using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using Integration.Salesforce.Library.Abstract;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Integration.Salesforce.Context
{
    public class DbContext<TModel> where TModel : AModel, new()
    {
        public const string MongoDbIdName = "_id";

        protected readonly IMongoClient _client;

        protected readonly IMongoDatabase _db;

        protected readonly IMongoCollection<TModel> _collection;

        protected readonly TimeSpan CacheExpiration;

        protected readonly TModel model = new TModel();

        public DbContext(IOptions<Settings> Settings)
        {
            //connection string
            string connectionString = Settings.Value.ConnectionString;
            
            MongoClientSettings settings = MongoClientSettings.FromUrl(
            new MongoUrl(connectionString)
            );
            
            settings.SslSettings = 
            new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            _client = new MongoClient(settings);

            _db = _client.GetDatabase(Settings.Value.Database);

            _collection = _db.GetCollection<TModel>(model.ModelType);
        }
    }
}