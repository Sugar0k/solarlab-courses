using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.IRepositories
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<List<User>> GetPaged(int offset, int limit, CancellationToken cancellationToken);
        Task<int> Count(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
        Task<User> GetByEmail(string email, CancellationToken cancellationToken);
        Task<User> GetByPhone(string phone, CancellationToken cancellationToken);
    }
}