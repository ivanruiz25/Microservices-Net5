using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Servicios.API.Libreria.Core.Config;
using Servicios.API.Libreria.Core.Entities.Class.Generic;
using Servicios.API.Libreria.Core.Entities.Interface.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.API.Libreria.Repository
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IOptions<MongoSettings> options)
        {
            MongoClient client = new(options.Value.ConectionString);
            IMongoDatabase mongoDB = client.GetDatabase(options.Value.DataBase);

            _collection = mongoDB.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute) documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()).CollectionName;
        }

        public async Task<IEnumerable<TDocument>> GetAll()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<TDocument> GetById(string id)
        {
            return await _collection.Find(Builders<TDocument>.Filter.Eq(doc => doc.Id, id)).SingleOrDefaultAsync();
        }

        public async Task InsertDocument(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task UpdateDocument(TDocument document)
        {
            await _collection.FindOneAndReplaceAsync(Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id), document);
        }

        public async Task DeleteById(string id)
        {
            await _collection.FindOneAndDeleteAsync(Builders<TDocument>.Filter.Eq(doc => doc.Id, id));
        }

        public async Task<PaginationEntity<TDocument>> PaginationByFilter(PaginationEntity<TDocument> paginationEntity)
        {
            // filtro para ordenar por ascendente o descendente
            SortDefinition<TDocument> sort = paginationEntity.SortDirection == "desc" ?
                Builders<TDocument>.Sort.Ascending(paginationEntity.Sort) :
                Builders<TDocument>.Sort.Descending(paginationEntity.Sort);

            // datos paginados y ordenados
            if(paginationEntity.FilterValue == null)
            {   // filtro
                paginationEntity.Data = await _collection.Find(p => true)
                    .Sort(sort)
                    .Skip((paginationEntity.Page - 1) * paginationEntity.PageSize)
                    .Limit(paginationEntity.PageSize)
                    .ToListAsync();

                // número total de registros en la base de datos
                paginationEntity.TotalRows = (await _collection.Find(p => true).ToListAsync()).Count;
            }
            else
            {   // con filtro
                FilterDefinition<TDocument> filter = 
                    Builders<TDocument>.Filter.Regex(paginationEntity.FilterValue.Propiedad, 
                        new BsonRegularExpression($".*{paginationEntity.FilterValue.Valor}.*", "1"));

                // obtenemos los registros
                paginationEntity.Data = await _collection.Find(filter)
                    .Sort(sort)
                    .Skip((paginationEntity.Page - 1) * paginationEntity.PageSize)
                    .Limit(paginationEntity.PageSize)
                    .ToListAsync();

                // número total de registros en la base de datos
                paginationEntity.TotalRows = (await _collection.Find(filter).ToListAsync()).Count;
            }

            // número de paginas
            paginationEntity.PageQuantity = 
                Convert.ToInt32(Math.Ceiling(paginationEntity.TotalRows / Convert.ToDecimal(paginationEntity.PageSize)));
            
            return paginationEntity;
        }
    }
}
