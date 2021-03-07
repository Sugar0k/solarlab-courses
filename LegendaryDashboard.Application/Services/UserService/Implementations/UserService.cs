using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Contracts.Contracts.User.Requests;
using LegendaryDashboard.Domain.Common;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.IRepositories;
using LegendaryDashboard.Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LegendaryDashboard.Application.Services.UserService.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly IHttpContextAccessor _accessor;

        public UserService(IUserRepository repository, IMapper mapper, IOptions<JwtOptions> jwtOptions, IHttpContextAccessor accessor)
        {
            _repository = repository;
            _mapper = mapper;
            _jwtOptions = jwtOptions;
            _accessor = accessor;
        }
        
        public async Task Register(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            Match phoneValidation  = Regex.
                Match(request.Phone,
                    @"^(?:\(?)(?<AreaCode>\d{3})(?:[\).\s]?)(?<Prefix>\d{3})(?:[-\.\s]?)(?<Suffix>\d{4})(?!\d)");
            
            Match emailValidation = Regex.
                Match(request.Email,
                "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}");
            
            if (!phoneValidation.Success)
            {
                throw new ValidationException("Неверный формат номера телефона");   
            }

            if (!emailValidation.Success)
            {
                throw new ValidationException("Неверный формат электронной почты");
            }
            
            var user = _mapper.Map<User>(request);
            user.Role = RoleConstants.UserRole;
            user.PasswordHash = Hashing.GetHash(request.Password);
            user.RegisterDate = DateTime.UtcNow;
            await _repository.Save(user, cancellationToken);
            
        }

        public async Task<string> Login(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByEmail(request.Email, cancellationToken);
            if (!user.PasswordHash.Equals(Hashing.GetHash(request.Password)))
            {
                throw new Exception("Неверный email или пароль!");
            }
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                new Claim(ClaimTypes.Role, user.Role)
            };
            var bytes = Encoding.ASCII.GetBytes(_jwtOptions.Value.Key);
                var expires = DateTime.UtcNow.AddMinutes(90);
                var securityKey = new SymmetricSecurityKey(bytes);
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
                {
                    Expires = expires,
                    SigningCredentials = credentials,
                    Subject = new ClaimsIdentity(claims),
                });
                
            return tokenHandler.WriteToken(token);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            if (ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor, id))
            {
                await _repository.Delete(id, cancellationToken);  
            }
        }
        public async Task<PagedResponce<UserDto>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            var users = await _repository.GetPaged(offset, limit, cancellationToken);
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return new PagedResponce<UserDto>
            {
                Count = usersDto.Count,
                EntityList = usersDto
            };
        }
        public async Task<int> Count(CancellationToken cancellationToken)
        {
            return await _repository.Count(cancellationToken);
        }
        public async Task<UserDto> FindById(int id, CancellationToken cancellationToken)
        {
            var user = await _repository.FindById(id, cancellationToken);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByEmail(string email, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByEmail(email, cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> GetByPhone(string phone, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByPhone(phone, cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
    }

    public static class Hashing
    {
        public static string GetHash(string str)
        {
            var strBytes = Encoding.ASCII.GetBytes(str);
            var sha = new SHA256Managed();
            var hash = sha.ComputeHash(strBytes);
            var hashedStr = Encoding.Default.GetString(hash);
            return hashedStr;
        }
    }
}