using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.IRepositories
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> GetByEmail(string email, CancellationToken cancellationToken);
        Task<User> GetByPhone(string phone, CancellationToken cancellationToken);
    }
}