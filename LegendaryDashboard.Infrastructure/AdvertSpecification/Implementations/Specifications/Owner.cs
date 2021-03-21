using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Xml.Schema;
using LegendaryDashboard.Contracts.Contracts.UserAdvert.Requests;
using LegendaryDashboard.Domain.Common;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.IRepositories;

namespace LegendaryDashboard.Infrastructure.AdvertSpecification.Implementations.Specifications
{
    public class Owner: Specification<Advert>
    {
        protected int OwnerId { get; }

        public Owner(int ownerId)
        {
            OwnerId = ownerId;
        }

        public override Expression<Func<Advert, bool>> ToExpression()
        {
            return x => x.UsersAdverts.Any(y => 
                y.UserId == OwnerId && 
                y.ConnectionType == AdvertUserConnectionTypes.OwnerConnection);
        }
        public static Owner New(int ownerId) => new Owner(ownerId);
    }
}