using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.ViewModels.RequestModels;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Infrastructure;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BlazorSozluk.Api.Application.Features.Commands.User
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await userRepository.GetSingleAsync(i=> i.EmailAddress == request.EmailAdress);
            if (dbUser == null)
            
                throw new DatabaseValidationException("User not found");

            var pass = PasswordEncryptor.Encrpt(request.Password);
            if (dbUser.Password != pass)
                throw new DatabaseValidationException("Password is wrong");

            if (!dbUser.EmailConfirmed)
                throw new DatabaseValidationException("Email adress is not confirmed yet");

            var result = mapper.Map<LoginUserViewModel>(dbUser);

            var claims = new Claim[]
           {
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                new Claim(ClaimTypes.Email, dbUser.EmailAddress),
                new Claim(ClaimTypes.Name, dbUser.UserName),
                new Claim(ClaimTypes.GivenName, dbUser.FirstName),
                new Claim(ClaimTypes.Surname, dbUser.LastName),
           };

            result.Token = GenerateToken(claims);
            return result;
        }

        public string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(10);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiry,
                signingCredentials: creds,
                notBefore: DateTime.Now);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }


    }
}
