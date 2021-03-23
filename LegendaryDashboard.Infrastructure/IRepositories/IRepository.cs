using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.IRepositories
{
    public interface IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        Task Save(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> FindById(TId id, CancellationToken cancellationToken);
        Task<int> Count(CancellationToken cancellationToken);
        Task<PagedResponse<TEntity>> GetPaged(int offset, int limit, CancellationToken cancellationToken);
        Task<PagedResponse<TEntity>> GetPaged(Expression<Func<TEntity, bool>> predicate, 
            int offset, int limit, CancellationToken cancellationToken);
        Task Delete(TId id, CancellationToken cancellationToken);
        Task Update(TEntity entity, CancellationToken cancellationToken);
    }
}