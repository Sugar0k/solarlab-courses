using System;
using System.Linq.Expressions;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.AdvertSpecification.Implementations.Specifications
{
    public class City: Specification<Advert>
    {
        protected string CityName { get; }

        public City(string city)
        {
            CityName = city;
        }
        public override Expression<Func<Advert, bool>> ToExpression()
        {
            return x => x.City == CityName;
        }
        public static City New(string city) => new City(city);
    }
}