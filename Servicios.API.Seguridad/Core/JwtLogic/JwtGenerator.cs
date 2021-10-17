using Microsoft.IdentityModel.Tokens;
using Servicios.API.Seguridad.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Servicios.API.Seguridad.Core.JwtLogic
{
    public class JwtGenerator : IJwtGenerator
    {
        public string CreateToken(Usuario usuario)
        {
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes("0QMvlA5tQCUSVzf24Q49LmIjjWUhCVbo"));

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new List<Claim>() 
                {
                    new Claim("username", usuario.UserName),
                    new Claim("nombre", usuario.Nombre),
                    new Claim("apellido", usuario.Apellido)
                }),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
            };

            JwtSecurityTokenHandler tokenHandler = new();
            
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
