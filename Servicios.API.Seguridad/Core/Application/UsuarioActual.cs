using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Servicios.API.Seguridad.Core.Dto;
using Servicios.API.Seguridad.Core.Entities;
using Servicios.API.Seguridad.Core.JwtLogic;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Servicios.API.Seguridad.Core.Application
{
    public class UsuarioActual
    {
        public class UsuarioActualCommand : IRequest<UsuarioDto> { }

        public class UsuarioActualHandler : IRequestHandler<UsuarioActualCommand, UsuarioDto>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IMapper _mapper;
            private readonly IUsuarioSesion _usuarioSesion;
            private readonly IJwtGenerator _generator;
            
            public UsuarioActualHandler(UserManager<Usuario> userManager, IUsuarioSesion usuarioSesion,
                IJwtGenerator generator, IMapper mapper)
            {
                _userManager = userManager;
                _usuarioSesion = usuarioSesion;
                _generator = generator;
                _mapper = mapper;
            }

            public async Task<UsuarioDto> Handle(UsuarioActualCommand request, CancellationToken cancellationToken)
            {
                Usuario usuario = await _userManager.FindByNameAsync(_usuarioSesion.GetUsuarioSession());

                if(usuario != null)
                {
                    UsuarioDto usuarioDto = _mapper.Map<Usuario, UsuarioDto>(usuario);
                    usuarioDto.Token = _generator.CreateToken(usuario);

                    return usuarioDto;
                }

                throw new Exception("El usuario no se encuentra en sesión");
            }
        }
    }
}
