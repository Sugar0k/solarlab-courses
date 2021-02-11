using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.Repository
{
    public interface IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        Task<TEntity> FindById(TId id, CancellationToken cancellationToken);
        Task Save(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> FindWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetPaged(int offset, int limit, CancellationToken cancellationToken);
    }
}