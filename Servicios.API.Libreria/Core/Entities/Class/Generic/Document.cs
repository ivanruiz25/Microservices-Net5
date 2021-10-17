using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Servicios.API.Libreria.Core.Entities.Interface.Generic;
using System;

namespace Servicios.API.Libreria.Core.Entities.Class.Generic
{
    public class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime CreateDate => DateTime.Now;
    }
}
