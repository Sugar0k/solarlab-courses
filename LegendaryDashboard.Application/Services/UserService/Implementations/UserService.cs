using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Contracts.Contracts.User.Requests;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;

namespace LegendaryDashboard.Application.Services.UserService.Implementations
{
    public sealed class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        private readonly IClaimsAccessor _claimsAccessor;
        private readonly IRepository<User, int> _repository;
        
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

            var user = await _repository.FindById(intId, cancellationToken);
            // if (user == null)
            // {
            //     throw new NoRightsException("Нет прав");
            // }
            return new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Phone = user.Phone,
                Email = user.Email,
                RegisterDate = user.RegisterDate,
                RoleId = user.RoleId
            };
        }

        public async Task<UserDto> Create(CreateUserRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            //TODO: Добавить автомаппер
            var user = new User
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email,
                PasswordHash = request.PasswordHash,
                RegisterDate = DateTime.UtcNow,
                RoleId = request.RoleId
            };

            await _repository.Save(user, cancellationToken);
            
            //TODO: Добавить автомаппер
            return new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Phone = user.Phone,
                Email = user.Email,
                RegisterDate = user.RegisterDate,
                RoleId = user.RoleId
            };
            
        }

        public async Task DeleteById(int Id, CancellationToken cancellationToken)
        {
            var user = _repository.FindById(Id, cancellationToken);
            if (user == null)
            {
                throw new Exception("Такого пользователя не существует");
            }

            await _repository.DeleteById(Id, cancellationToken);
            
        }
        
         public async Task<IQueryable<UserDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _repository.
        }
    }
}