using Microsoft.AspNetCore.Mvc;
using Servicios.API.Libreria.Core.Entities.Class;
using Servicios.API.Libreria.Core.Entities.Class.Generic;
using Servicios.API.Libreria.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios.API.Libreria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMongoRepository<AutorEntity> _autorEntity;

        public AutorController(IMongoRepository<AutorEntity> autorEntity)
        {
            _autorEntity = autorEntity;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> Get()
        {
            return Ok(await _autorEntity.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorEntity>> GetById(string id)
        {
            return Ok(await _autorEntity.GetById(id));
        }

        [HttpPost]
        public async Task Insert(AutorEntity autor)
        {
            await _autorEntity.InsertDocument(autor);
        }

        [HttpPut("{id}")]
        public async Task Update(string id, AutorEntity autor)
        {
            autor.Id = id;
            await _autorEntity.UpdateDocument(autor);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _autorEntity.DeleteById(id);
        }

        [HttpPost("pagination")]
        public async Task<ActionResult<PaginationEntity<AutorEntity>>> PostPagination(PaginationEntity<AutorEntity> pagination)
        {
            return Ok(await _autorEntity.PaginationByFilter(pagination));
        }
    }
}
