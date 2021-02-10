using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Contracts.Contracts.User.Requests;

namespace LegendaryDashboard.Application.Services.UserService.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetCurrent(CancellationToken cancellationToken);
        Task<UserDto> Create(CreateUserRequest request, CancellationToken cancellationToken);
        Task DeleteById(int Id, CancellationToken cancellationToken);
        Task<IQueryable<UserDto>> GetAll(CancellationToken cancellationToken);
    } 
}