using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Contracts.Contracts.User.Requests;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.IRepositories;

namespace LegendaryDashboard.Application.Services.UserService.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Register(CreateUserRequest request, CancellationToken cancellationToken)
        {
            //var user = _mapper.Map<User>(request);
            // TODO: настроить маппер на создание даты
            User user = new User
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
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id, cancellationToken);
        }
        public async Task<IEnumerable<UserDto>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            var users = await _repository.GetPaged(offset, limit, cancellationToken);
            return _mapper.Map<List<User>, List<UserDto>>(users);;
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
}