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
        [HttpGet("get_by_titles/{title}")]
        public async Task<IActionResult> GetByTitles(
            string title,
            CancellationToken cancellationToken
        )
        {
            var categories = await _categoryService.GetByTitles(title, cancellationToken);
            return Ok(categories);
        }
    }
}