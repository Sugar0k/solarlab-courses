using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.IRepositories
{
    public interface IAdvertRepository : IRepository<Advert, int>
    {
        Task<List<Advert>> GetByCategoryId(int categoryId, int offset, int limit, CancellationToken cancellationToken);
        Task<List<Advert>> GetByState(string state, int offset, int limit, CancellationToken cancellationToken);
        Task<List<Advert>> GetByCity(string city, int offset, int limit, CancellationToken cancellationToken);
        Task<List<Advert>> GetByTitle(string title, int offset, int limit, CancellationToken cancellationToken);
        Task<int> GetViewsCount(int id, CancellationToken cancellationToken);
        Task AddView(int id, CancellationToken cancellationToken);

    }
}