using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.Repository
{
    public interface IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        IQueryable<TEntity> AsQueryable();
        
        Task<TEntity> Save(TEntity entity, CancellationToken ct = default);
        Task<TEntity> FindById(TId Id, CancellationToken cancellationToken);
        Task DeleteById(TId Id, CancellationToken cancellationToken);
    }
}