using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IeltsApp.DataAccess.Models
{
    [BsonIgnoreExtraElements]
    public class Blog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("title")]
        public string Title { get; set; } = null!;
        [BsonElement("updateDate")]
        public string UpdateDate { get; set; } = null!;
        [BsonElement("author")]
        public string Author { get; set; } = null!;
        [BsonElement("image")]
        public string Image { get; set; } = null!;
        [BsonElement("article")]
        public string Article { get; set; } = null!;

    }
}
