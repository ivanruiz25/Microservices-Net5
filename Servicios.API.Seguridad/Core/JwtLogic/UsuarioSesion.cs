using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Servicios.API.Seguridad.Core.JwtLogic
{
    public class UsuarioSesion : IUsuarioSesion
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioSesion(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUsuarioSession()
        {
            return _httpContextAccessor.HttpContext.User?.Claims?
                .FirstOrDefault(x => x.Type == "username")?.Value;
        }
    }
}
