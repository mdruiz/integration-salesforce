using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using Integration.Salesforce.Library.Abstract;
using Integration.Salesforce.Library.Models;
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

        public DbContext(Settings Settings)
        {
            //connection string
            string connectionString = Settings.ConnectionString;
            
            MongoClientSettings settings = MongoClientSettings.FromUrl(
            new MongoUrl(connectionString)
            );
            
            settings.SslSettings = 
            new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            _client = new MongoClient(settings);

            _db = _client.GetDatabase(Settings.Database);

            _collection = _db.GetCollection<TModel>(model.ModelType);
        }
        public void UpdateMongoDB(IEnumerable<TModel> dataContacts)
        {
            // Get the contacts in the Person collection, check for existing contacts.
            // If not present, add to collection.
            var mongoContacts = _collection.Find(_ => true).ToList();
            foreach (TModel dataContact in dataContacts)
            {
                var existingContact = mongoContacts.Find(item => dataContact.ModelId == item.ModelId);

                if (existingContact == null)
                {
                    _collection.InsertOne(dataContact);
                }
            }

            // Next, if the contacts in the MongoDB does not exist in the salesforce API data, then
            // remove it from the MongoDB.
            List<TModel> dataContactList = new List<TModel>(dataContacts);
            foreach (var mongoContact in mongoContacts)
            {
                var existingContact = dataContactList.Find(item => mongoContact.ModelId == item.ModelId);
                if (existingContact == null)
                {
                    var builder = Builders<TModel>.Filter;
                    var filter = builder.Eq (x => x.ModelId, mongoContact.ModelId);
                    _collection.DeleteMany(filter);
                }
            }
        }
        public void UpdateMongoEntries(IEnumerable<TModel> dataContacts)
        {
            var mongoContacts = _collection.Find(_ => true).ToList();
            foreach (TModel dataContact in dataContacts)
            {
                var existingContact = mongoContacts.Find(item => dataContact.ModelId == item.ModelId);

                if (existingContact != null)
                {
                    var builder = Builders<TModel>.Filter;
                    var filter = builder.Eq (x => x.ModelId, dataContact.ModelId);
                    
                    _collection.ReplaceOne(filter, dataContact);
                }
            }
        }
        public TModel GetModelById(string modelId)
        {
            return _collection.Find(item => item.ModelId == modelId).FirstOrDefault();
        }
        public void DeleteMongoEntries(IEnumerable<TModel> dataContacts)
        {
            var mongoContacts = _collection.Find(_ => true).ToList();
            foreach (TModel dataContact in dataContacts)
            {
                var existingContact = mongoContacts.Find(item => dataContact.ModelId == item.ModelId);

                if (existingContact != null)
                {
                    var builder = Builders<TModel>.Filter;
                    var filter = builder.Eq (x => x.ModelId, dataContact.ModelId);
                    
                    _collection.DeleteOne(filter);
                }
            }
        }
        public IEnumerable<TModel> ReadMongoEntries()
        {
             return _collection.Find(item => item.Active).ToList();
        }
        public void RemoveAllMongoEntries()
        {
            var builder = Builders<TModel>.Filter;
            var filter = builder.Empty;
            _collection.DeleteMany(filter);
        }
        public void RemoveMongoEntries(IEnumerable<TModel> dataContacts)
        {
            var builder = Builders<TModel>.Filter;
            foreach(var item in dataContacts)
            {
                var filter = builder.Eq(x => x.ModelId, item.ModelId);
                _collection.DeleteMany(filter);
            }
        }
    }
}