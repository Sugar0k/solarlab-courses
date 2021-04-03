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
    public class Follower: Specification<Advert>
    {
        private int FollowerId { get; }

        public Follower(int followerId)
        {
            FollowerId = followerId;
        }

        public override Expression<Func<Advert, bool>> ToExpression()
        {
            return x => x.UsersAdverts.Any(y => 
                y.UserId == FollowerId && 
                y.ConnectionType == AdvertUserConnectionTypes.OwnerConnection);
        }
        public static Follower New(int followerId) => new Follower(followerId);
    }
}