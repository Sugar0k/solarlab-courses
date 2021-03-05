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
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            int offset, int limit,
            [FromServices] ICategoryService service,
            CancellationToken cancellationToken
        )
        {
            var categories = await service.GetPaged(offset, limit, cancellationToken);
            return Ok(categories);
        }
    }
}