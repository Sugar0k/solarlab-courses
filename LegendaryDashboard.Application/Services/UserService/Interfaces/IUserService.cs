using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Contracts.Contracts.User.Requests;
using LegendaryDashboard.Domain.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace LegendaryDashboard.Application.Services.UserService.Interfaces
{
    public interface IUserService
    {
        Task Register(RegisterUserRequest request, CancellationToken cancellationToken);
        Task<string> Login(LoginUserRequest request, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<PagedResponse<UserDto>> GetPaged(int offset, int limit, CancellationToken cancellationToken);
        Task<int> Count(CancellationToken cancellationToken);
        Task<UserDto> FindById(int id, CancellationToken cancellationToken);
        Task<UserDto> GetByEmail(string email, CancellationToken cancellationToken);
        Task<UserDto> GetByPhone(string phone, CancellationToken cancellationToken);
        Task Update(UpdateUserRequest userDto, CancellationToken cancellationToken);
        Task UpdatePassword(int userId, string oldPassword, string newPassword, CancellationToken cancellationToken);
    }
}