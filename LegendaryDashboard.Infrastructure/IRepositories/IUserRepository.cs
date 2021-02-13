using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.IRepositories
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<List<User>> GetPaged(int offset, int limit, CancellationToken cancellationToken);
    }
}