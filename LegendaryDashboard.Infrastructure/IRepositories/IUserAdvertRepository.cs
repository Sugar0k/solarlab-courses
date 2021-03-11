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
        Task<List<UserAdvert>> GetPaged(int offset, int limit, CancellationToken cancellationToken);
        Task<List<UserAdvert>> GetConnectionsByUserId(GetConnectionsRequest request, CancellationToken cancellationToken);
        Task<List<UserAdvert>> GetConnectionsByAdvertId(GetConnectionsRequest request, CancellationToken cancellationToken);
        Task<List<UserAdvert>> GetConnectionsByAdvertIdAndType(
            GetConnectionsWithTypeRequest request,
            CancellationToken cancellationToken);
        Task<List<UserAdvert>> GetConnectionsByUserIdAndType(
            GetConnectionsWithTypeRequest request, 
            CancellationToken cancellationToken);
        Task DeleteByAdvertId(int advertId, CancellationToken cancellationToken);
        Task DeleteByUserId(int userId, CancellationToken cancellationToken);
    }
}