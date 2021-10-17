using AutoMapper;
using Servicios.API.Seguridad.Core.Entities;

namespace Servicios.API.Seguridad.Core.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>();
        }
    }
}
