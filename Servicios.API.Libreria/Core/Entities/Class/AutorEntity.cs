using MongoDB.Bson.Serialization.Attributes;
using Servicios.API.Libreria.Core.Entities.Class.Generic;

namespace Servicios.API.Libreria.Core.Entities.Class
{
    [BsonCollection("Autor")]
    public class AutorEntity : Document
    {
        [BsonElement("nombre")]
        public string Nombre { get; set; }
        [BsonElement("apellido")]
        public string Apellido { get; set; }
        [BsonElement("gradoAcademico")]
        public string GradoAcademico { get; set; }
        [BsonElement("nombreCompleto")]
        public string NombreCompleto { get; set; }
    }
}
