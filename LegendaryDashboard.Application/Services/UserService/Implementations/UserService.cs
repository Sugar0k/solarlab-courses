using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LegendaryDashboard.Infrastructure.Repository;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Contracts.Contracts.User.Requests;
using LegendaryDashboard.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace LegendaryDashboard.Application.Services.UserService.Implementations
{
    public class UserService : IUserService
    {
        private readonly IClaimsAccessor _claimsAccessor;
        private readonly IRepository<Domain.Models.User, int> _repository;
        private readonly IMapper _mapper;

        public UserService(IClaimsAccessor claimsAccessor, IRepository<User, int> repository, IMapper mapper)
        {
            _claimsAccessor = claimsAccessor;
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<UserDto> GetCurrent(CancellationToken cancellationToken)
        {
            var claim = (await _claimsAccessor.GetCurrentClaims(cancellationToken))
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;
            if (string.IsNullOrWhiteSpace(claim))
            {
                return null;
            }
            var intId = int.Parse(claim);
            User user = await _repository.FindById(intId, cancellationToken);
            if (user == null)
            {
                throw new Exception("Нет прав");
            }
            return _mapper.Map<UserDto>(user);
        }

        public Task<LoginResponce> Login(LoginUserRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /*public async Task<LoginResponce> Login(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _repository.FindWhere(u => u.Email == request.Email, cancellationToken);
            if (user == null || !user.PasswordHash.Equals(request.PasswordHash))
            {
                throw new Exception("Нет прав");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, request.Email),
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                    SecurityAlgorithms.HmacSha256)
            );

            return new LoginResponce()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }*/

        public async Task<UserDto> Register(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            await _repository.Save(user, cancellationToken);

            return _mapper.Map<UserDto>(user);
        }
    }
}