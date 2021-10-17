using Servicios.API.Seguridad.Core.Entities;

namespace Servicios.API.Seguridad.Core.JwtLogic
{
    public interface IJwtGenerator
    {
        string CreateToken(Usuario usuario);
    }
}
