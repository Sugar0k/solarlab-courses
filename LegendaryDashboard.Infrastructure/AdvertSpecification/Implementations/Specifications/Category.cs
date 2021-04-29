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

        private int category { get; }
        
        public override Expression<Func<Advert, bool>> ToExpression()
        {
            //Не бейте, не разобрался с Expressions
            return x =>
                x.CategoryId == category ||
                x.Category.ParentCategoryId == category ||
                x.Category.ParentCategory.ParentCategoryId == category ||
                x.Category.ParentCategory.ParentCategory.ParentCategoryId == category ||
                x.Category.ParentCategory.ParentCategory.ParentCategory.ParentCategoryId == category ||
                x.Category.ParentCategory.ParentCategory.ParentCategory.ParentCategory.ParentCategoryId == category;

        }
        public static Category New(int c) => new Category(c);
    }
}