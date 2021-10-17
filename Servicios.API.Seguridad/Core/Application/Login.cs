using AutoMapper;
using FluentValidation;
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
    public class Login
    {
        public class UsuarioLoginCommand : IRequest<UsuarioDto>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UsuarioLoginValidation : AbstractValidator<UsuarioLoginCommand>
        {
            public UsuarioLoginValidation()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class UsuarioLoginHandle : IRequestHandler<UsuarioLoginCommand, UsuarioDto>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IMapper _mapper;
            private readonly IJwtGenerator _generator;
            private readonly SignInManager<Usuario> _signInManager;

            public UsuarioLoginHandle(UserManager<Usuario> userManager, 
                IMapper mapper, IJwtGenerator generator, SignInManager<Usuario> signInManager)
            {
                _userManager = userManager;
                _mapper = mapper;
                _generator = generator;
                _signInManager = signInManager;
            }

            public async Task<UsuarioDto> Handle(UsuarioLoginCommand request, CancellationToken cancellationToken)
            {
                Usuario usuario = await _userManager.FindByEmailAsync(request.Email);

                if(usuario == null)
                {
                    throw new Exception("El usuario no existe");
                }

                var result = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                if (result.Succeeded)
                {
                    UsuarioDto usuarioDto = _mapper.Map<Usuario, UsuarioDto>(usuario);
                    usuarioDto.Token = _generator.CreateToken(usuario);

                    return usuarioDto;
                }

                throw new Exception("La contraseña es incorrecta");
            }
        }
    }
}
