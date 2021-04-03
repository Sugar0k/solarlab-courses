using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.DbContext;
using LegendaryDashboard.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LegendaryDashboard.Application.Services.Repositories
{
    public class AdvertImageRepository : Repository<AdvertImage, string>, IAdvertImageRepository
    {
        public AdvertImageRepository(DashboardContext context) : base(context)
        {
        }
        

        public async Task<IEnumerable<AdvertImage>> GetByAdvertId(int advertId, CancellationToken cancellationToken)
        {
            return await DbSet.Where(a => a.AdvertId == advertId)
                .OrderBy(a => a.DateCreate)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteByAdvertId(int advertId, CancellationToken cancellationToken)
        {
            DbSet.RemoveRange(DbSet.Where(a => a.AdvertId == advertId).ToArray());
            await Context.SaveChangesAsync(cancellationToken);
        }
        
    }
}