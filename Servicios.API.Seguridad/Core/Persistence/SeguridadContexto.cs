using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Servicios.API.Seguridad.Core.Entities;

namespace Servicios.API.Seguridad.Core.Persistence
{
    public class SeguridadContexto: IdentityDbContext<Usuario>
    {
        public SeguridadContexto(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
