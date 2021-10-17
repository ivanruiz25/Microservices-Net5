using MongoDB.Bson.Serialization.Attributes;
using Servicios.API.Libreria.Core.Entities.Class.Generic;
using System;

namespace Servicios.API.Libreria.Core.Entities.Class
{
    [BsonCollection("Libro")]
    public class LibroEntity : Document
    {
        [BsonElement("titulo")]
        public string Titulo { get; set; }

        [BsonElement("descripcion")]
        public string Descripcion { get; set; }

        [BsonElement("precio")]
        public int Precio { get; set; }

        [BsonElement("fechaPublicacion")]
        public DateTime? FechaPublicacion { get; set; }

        [BsonElement("autor")]
        public AutorEntity Autor { get; set; }
    }
}
