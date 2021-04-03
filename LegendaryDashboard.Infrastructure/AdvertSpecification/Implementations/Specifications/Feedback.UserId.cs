using System;
using System.Linq;
using System.Linq.Expressions;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.AdvertSpecification.Implementations.Specifications
{
    public class UserId: Specification<Feedback>
    {
        public UserId(int c)
        {
            userId = c;
        }

        private int userId { get; }
        
        public override Expression<Func<Feedback, bool>> ToExpression()
        {
            return x =>
                x.UserId == userId;
        }
        public static UserId New(int c) => new UserId(c);
    }
}