using System;
using System.Linq.Expressions;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.AdvertSpecification.Implementations.Specifications
{
    public class Title: Specification<Advert>
    {
        private string title { get; }

        public Title(string t)
        {
            title = t;
        }
        public override Expression<Func<Advert, bool>> ToExpression()
        {
            return x => x.Title.Contains(title);
        }
        public static Title New(string t) => new Title(t);
    }
}