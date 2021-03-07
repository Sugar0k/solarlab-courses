using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.IRepositories
{
    public interface IAdvertImageRepository : IRepository<AdvertImage, int>
    {
        Task<IEnumerable<AdvertImage>> GetByAdvertId(int advertId, CancellationToken cancellationToken);
        Task<IEnumerable<AdvertImage>> GetByImageId(string imageGuid, CancellationToken cancellationToken);
        Task DeleteByAdvertId(int advertId, CancellationToken cancellationToken);
        Task DeleteByImageId(string imagGuid, CancellationToken cancellationToken);
    }
}