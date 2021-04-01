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
        [HttpGet("count")]
        public async Task<IActionResult> GetCount(
            CancellationToken cancellationToken)
        {
            var count = await _categoryService.Count(cancellationToken);
            return Ok(count);
        }
    }
}