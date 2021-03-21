using System;
using System.Linq;
using System.Linq.Expressions;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.AdvertSpecification.Implementations.Specifications
{
    public class Category: Specification<Advert>
    {
        public Category(int c)
        {
            category = c;
        }

        protected int category { get; }
        
        public override Expression<Func<Advert, bool>> ToExpression()
        {
            return x => 
                x.CategoryId == category || 
                x.Category.ParentCategoryId == category;
        }
        public static Category New(int c) => new Category(c);
    }
}