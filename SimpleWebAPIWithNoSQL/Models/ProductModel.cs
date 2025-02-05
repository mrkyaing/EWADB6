
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleWebAPIWithNoSQL.Models
{
    public class ProductModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public  string? Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal UnitPrice { get; set; }
        public int  Stock { get; set; }

    }
}