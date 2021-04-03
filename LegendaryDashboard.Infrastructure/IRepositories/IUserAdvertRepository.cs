using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts.UserAdvert.Requests;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.IRepositories
{
    public interface IUserAdvertRepository : IRepository<UserAdvert, int>
    {
        public Task<int> GetOwnerId(int advertId, CancellationToken cancellationToken);
        Task<List<UserAdvert>> GetConnections(
            Expression<Func<UserAdvert, bool>> predicate, 
            CancellationToken cancellationToken);
        Task DeleteByAdvertId(int advertId, CancellationToken cancellationToken);
        Task DeleteByUserId(int userId, CancellationToken cancellationToken);
    }
}