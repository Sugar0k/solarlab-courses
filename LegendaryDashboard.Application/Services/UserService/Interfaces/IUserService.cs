using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Contracts.Contracts.User.Requests;

namespace LegendaryDashboard.Application.Services.UserService.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetCurrent(CancellationToken cancellationToken);
        Task<LoginResponce> Login(LoginUserRequest request, CancellationToken cancellationToken);
        Task<UserDto> Register(CreateUserRequest request, CancellationToken cancellationToken);
    }
}