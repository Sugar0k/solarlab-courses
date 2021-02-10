using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Application
{
    public interface IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        IQueryable<TEntity> AsQueryable();
        
        Task<TEntity> Save(TEntity entity, CancellationToken ct = default);
    }
}