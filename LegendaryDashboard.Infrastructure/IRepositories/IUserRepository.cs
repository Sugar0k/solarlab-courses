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
        public Task Update(User user, CancellationToken cancellationToken);
        
        Task<User> GetByEmail(string email, CancellationToken cancellationToken);
        Task<User> GetByEmailAndPass(string email, string pass, CancellationToken cancellationToken);
        Task<User> GetByPhone(string phone, CancellationToken cancellationToken);
        Task<bool> EmailExist(string email, CancellationToken cancellationToken);
        Task<bool> PhoneExist(string phone, CancellationToken cancellationToken);
        Task<User> FindByAdvertId(int id, CancellationToken cancellationToken);
    }
}