using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Servicios.API.Seguridad.Core.Dto;
using Servicios.API.Seguridad.Core.Entities;
using Servicios.API.Seguridad.Core.JwtLogic;
using Servicios.API.Seguridad.Core.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servicios.API.Seguridad.Core.Application
{
    public class Register
    {
        public class UsuarioRegisterCommand : IRequest<UsuarioDto>
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UsuarioRegisterValidation : AbstractValidator<UsuarioRegisterCommand>
        {
            public UsuarioRegisterValidation()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class UsuarioRegisterHandler : IRequestHandler<UsuarioRegisterCommand, UsuarioDto>
        {
            private readonly SeguridadContexto _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IMapper _mapper;
            private readonly IJwtGenerator _generator;

            public UsuarioRegisterHandler(SeguridadContexto context, UserManager<Usuario> userManager, 
                IMapper mapper, IJwtGenerator generator)
            {
                _context = context;
                _userManager = userManager;
                _mapper = mapper;
                _generator = generator;
            }

            public async Task<UsuarioDto> Handle(UsuarioRegisterCommand request, CancellationToken cancellationToken)
            {
                var exist = await _context.Users.Where(x => x.Email == request.Email).AnyAsync(cancellationToken: cancellationToken);

                if (exist)
                {
                    throw new Exception("El email del usuario ya existe en la base de datos");
                }

                exist = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync(cancellationToken: cancellationToken);

                if (exist)
                {
                    throw new Exception("El nombre del usuario ya existe en la base de datos");
                }

                Usuario usuario = new()
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    Email = request.Email,
                    UserName = request.UserName
                };

                var resultado = await _userManager.CreateAsync(usuario, request.Password);

                if (resultado.Succeeded)
                {
                    UsuarioDto result = _mapper.Map<Usuario, UsuarioDto>(usuario);
                    result.Token = _generator.CreateToken(usuario);
                    
                    return result;
                }

                throw new Exception("No se pudo registrar el usuario");
            }
        }
    }
}
