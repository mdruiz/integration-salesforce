using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;

namespace Integration.Salesforce.Library.Abstract
{
    public abstract class AModel
    {
        /// <summary>
        /// This is an ObjectId in MongoDB and string when exposing to API
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ModelId { get; set; }

        public DateTime LastModified { get; set; }

        /// <summary> 
        /// 
        /// </summary>
        public abstract void MapJsonToModel(JObject jsonObject);


    }
}