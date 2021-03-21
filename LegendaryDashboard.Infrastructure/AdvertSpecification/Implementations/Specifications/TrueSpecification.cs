using System;
using System.Linq.Expressions;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.AdvertSpecification.Implementations.Specifications
{
    public class TrueSpecification<T>: Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
        public static TrueSpecification<T> New() => new TrueSpecification<T>();
    }
}