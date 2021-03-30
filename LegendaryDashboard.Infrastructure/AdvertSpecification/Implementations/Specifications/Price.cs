using System;
using System.Linq.Expressions;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.AdvertSpecification.Implementations.Specifications
{
    public class Price: Specification<Advert>
    {
        public Price(int min, int max)
        {
            Min = min;
            Max = max;
        }

        private int Min { get; }
        private int Max { get; }
        
        public override Expression<Func<Advert, bool>> ToExpression()
        {
            return x => x.Price >= Min && x.Price <= Max;
        }
        public static Price New(int min, int max) => new Price(min, max);
    }
}