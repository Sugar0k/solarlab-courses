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

        private bool PhoneChecker(string phoneNumber)
        {
            Match phoneValidation  = Regex.
                Match(phoneNumber,
                    @"^(?:\(?)(?<AreaCode>\d{3})(?:[\).\s]?)(?<Prefix>\d{3})(?:[-\.\s]?)(?<Suffix>\d{4})(?!\d)");

            return phoneValidation.Success;
        }

        private bool EmailChecker(string email)
        {
            Match emailValidation = Regex.
                Match(email,
                    "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}");
            
            return emailValidation.Success;
        }

        public Task Update(int userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task Register(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            if (!PhoneChecker(request.Phone))
            {
                throw new ValidationException("Неверный формат номера телефона");   
            }

            if (!EmailChecker(request.Email))
            {
                throw new ValidationException("Неверный формат электронной почты");
            }

            if (await GetByEmail(request.Email, cancellationToken) != null) 
                throw new Exception("Пользователь с таким email уже существует");
            if (await GetByPhone(request.Phone, cancellationToken) != null) 
                throw new Exception("Пользователь с таким номером телефона уже существует");
            var user = _mapper.Map<User>(request);
            user.Role = RoleConstants.UserRole;
            user.PasswordHash = Hashing.GetHash(request.Password);
            user.RegisterDate = DateTime.UtcNow;
            await _repository.Save(user, cancellationToken);
            
        }

        public async Task<string> Login(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByEmail(request.Email, cancellationToken);
            if (user == null || !user.PasswordHash.Equals(Hashing.GetHash(request.Password)))
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
        public async Task<PagedResponse<UserDto>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            var users = await _repository.GetPaged(offset, limit, cancellationToken);
            var usersDto = _mapper.Map<List<UserDto>>(users.EntityList);
            return new PagedResponse<UserDto>
            {
                Count = users.Count,
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

        public async Task Update(UserDto userDto, CancellationToken cancellationToken)
        {
            if (!PhoneChecker(userDto.Phone))
            {
                throw new ValidationException(
                    "Невозможно изменить номер телефона так как изменение имеет неверный формат"
                    );   
            }

            if (!EmailChecker(userDto.Email))
            {
                throw new ValidationException(
                    "Невозможно изменить адрес электронной почты так как изменение имеет неверный формат"
                    );
            }

            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = (await _repository.FindById(userDto.Id, cancellationToken)).PasswordHash;
            await _repository.Update(user, cancellationToken);
        }
        
        public async Task UpdatePassword(int userId, string oldPassword, string newPassword, CancellationToken cancellationToken)
        {
            var userWithNewPassword = await _repository.FindById(userId, cancellationToken);
            
            if (userWithNewPassword == null) 
                throw new Exception("Пользователь не найден!");
            if (!ClaimsPrincipalExtensions.IsAdminOrOwner(_accessor, userId)) 
                throw new Exception("Нет прав!");
            if (!userWithNewPassword.PasswordHash.Equals(Hashing.GetHash(oldPassword))) 
                throw new Exception("Неверный пароль!");
            
            userWithNewPassword.PasswordHash = Hashing.GetHash(newPassword);
            
            await _repository.Update(
                userWithNewPassword, cancellationToken
            );
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