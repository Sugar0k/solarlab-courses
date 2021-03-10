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
        [Authorize(Roles = RoleConstants.AdminRole)]
        [HttpGet("get_by_parent_id/{id}")]
        public async Task<IActionResult> GetByParentCategoryId(
            int id,
            [FromServices] ICategoryService service,
            CancellationToken cancellationToken
        )
        {
            var categories = await service.GetByParentCategoryId(id, cancellationToken);
            return Ok(categories);
        }
    }
}