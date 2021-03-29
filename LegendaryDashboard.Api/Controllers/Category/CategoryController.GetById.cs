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
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(
            int id,
            [FromServices] ICategoryService service,
            CancellationToken cancellationToken
        )
        {
            var category = await service.FindById(id, cancellationToken);
            return Ok(category);
        }
    }
}