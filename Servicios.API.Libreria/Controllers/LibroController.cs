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
    public class LibroController : ControllerBase
    {
        private readonly IMongoRepository<LibroEntity> _libroRepository;

        public LibroController(IMongoRepository<LibroEntity> libroRepository)
        {
            _libroRepository = libroRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroEntity>>> Get()
        {
            return Ok(await _libroRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroEntity>> GetById(string id)
        {
            return Ok(await _libroRepository.GetById(id));
        }

        [HttpPost]
        public async Task Insert(LibroEntity libro)
        {
            await _libroRepository.InsertDocument(libro);
        }

        [HttpPost("pagination")]
        public async Task<ActionResult<PaginationEntity<LibroEntity>>> PostPagination(PaginationEntity<LibroEntity> pagination)
        {
            return Ok(await _libroRepository.PaginationByFilter(pagination));
        }
    }

}
