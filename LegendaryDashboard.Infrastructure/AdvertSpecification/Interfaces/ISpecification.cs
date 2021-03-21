using System;
using System.Linq.Expressions;

namespace LegendaryDashboard.Infrastructure.AdvertSpecification.Interfaces
{
    public interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> ToExpression();

        bool IsSatisfiedBy(TEntity entity);
    }
}