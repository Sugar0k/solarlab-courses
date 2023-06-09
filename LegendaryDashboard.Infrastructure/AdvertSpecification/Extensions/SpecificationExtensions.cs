﻿using LegendaryDashboard.Infrastructure.AdvertSpecification.Implementations;
using LegendaryDashboard.Infrastructure.AdvertSpecification.Interfaces;

namespace LegendaryDashboard.Infrastructure.AdvertSpecification.Extensions
{
    public static class SpecificationExtensions
    {
        public static Specification<TEntity> And<TEntity>
            (this ISpecification<TEntity> current, ISpecification<TEntity> specification)
            => new AndSpecification<TEntity>(current, specification);

        public static Specification<TEntity> Or<TEntity>
            (this ISpecification<TEntity> current, ISpecification<TEntity> specification)
            => new OrSpecification<TEntity>(current, specification);

        public static Specification<TEntity> Not<TEntity>(this ISpecification<TEntity> current)
            => new NotSpecification<TEntity>(current);
    }
}