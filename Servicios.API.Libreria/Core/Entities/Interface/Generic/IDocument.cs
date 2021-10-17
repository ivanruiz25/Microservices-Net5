using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Servicios.API.Libreria.Core.Entities.Interface.Generic
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }
        DateTime CreateDate { get; }
    }
}
