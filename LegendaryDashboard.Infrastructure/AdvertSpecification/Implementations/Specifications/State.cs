using System;
using System.Linq.Expressions;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.AdvertSpecification.Implementations.Specifications
{
    public class State: Specification<Advert>
    {
        protected string StateName { get; }

        public State(string state)
        {
            StateName = state;
        }
        public override Expression<Func<Advert, bool>> ToExpression()
        {
            return x => x.State == StateName;
        }
        public static State New(string state) => new State(state);
    }
}