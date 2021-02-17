using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
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
            user.RegisterDate = DateTime.UtcNow;
            await _repository.Save(user, cancellationToken);
            
        }
        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id, cancellationToken);
        }
        public async Task<IEnumerable<UserDto>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            var users = await _repository.GetPaged(offset, limit, cancellationToken);
            return _mapper.Map<List<UserDto>>(users);
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