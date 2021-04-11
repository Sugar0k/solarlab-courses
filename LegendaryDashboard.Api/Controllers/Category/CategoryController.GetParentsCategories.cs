using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.CategoryService.Interfaces;
using LegendaryDashboard.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Category
{
    public partial class CategoryController
    {
        [HttpGet("get_parents")]
        public async Task<IActionResult> GetParentsCategories(
            int limit, int offset,
            CancellationToken cancellationToken
        )
        {
            var categories = await _categoryService.GetParentsCategories(limit, offset,cancellationToken);
            return Ok(categories);
        }
    }
}