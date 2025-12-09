using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IIG.Core.Providers.MongoDbProvider.Models
{
    public abstract class Document
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
