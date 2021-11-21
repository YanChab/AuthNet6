using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuthNet6.Shared
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string Role { get; set; }="";
        public DateTime CreationDate { get; set; }
    }
}