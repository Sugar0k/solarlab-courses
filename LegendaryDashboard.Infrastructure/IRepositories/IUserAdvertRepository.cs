using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.IRepositories
{
    public interface IUserAdvertRepository : IRepository<UserAdvert, int>
    {
        Task<List<UserAdvert>> GetPaged(int offset, int limit, CancellationToken cancellationToken);
        Task<List<UserAdvert>> GetConnectionsByUserId(int userId, int offset, int limit, CancellationToken cancellationToken);
        Task<List<UserAdvert>> GetConnectionsByAdvertId(int advertId, int offset, int limit, CancellationToken cancellationToken);
        Task<List<UserAdvert>> GetConnectionsByAdvertIdAndType(
            int advertId,
            string type, 
            int offset, 
            int limit, 
            CancellationToken cancellationToken);
        Task<List<UserAdvert>> GetConnectionsByUserIdAndType(
            int userId,
            string type, 
            int offset, 
            int limit, 
            CancellationToken cancellationToken);
        Task<int> Count(Expression<Func<UserAdvert, bool>> predicate, CancellationToken cancellationToken);

    }
}