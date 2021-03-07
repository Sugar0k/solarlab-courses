using System.Collections.Generic;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Contracts.Contracts
{
    public class PagedResponce<TEntity>
    {
        public int Count { get; set; }
        public List<TEntity> EntityList { get; set; }
    }
}