using System.Collections.Generic;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Contracts.Contracts
{
    public class PagedResponse<TEntity>
    {
        public int Count { get; set; }
        public ICollection<TEntity> EntityList { get; set; }
    }
}