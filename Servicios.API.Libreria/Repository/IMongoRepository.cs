using Servicios.API.Libreria.Core.Entities.Class.Generic;
using Servicios.API.Libreria.Core.Entities.Interface.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios.API.Libreria.Repository
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<IEnumerable<TDocument>> GetAll();
        Task<TDocument> GetById(string id);
        Task InsertDocument(TDocument document);
        Task UpdateDocument(TDocument document);
        Task DeleteById(string id);
        Task<PaginationEntity<TDocument>> PaginationByFilter(PaginationEntity<TDocument> paginationEntity);
    }
}
