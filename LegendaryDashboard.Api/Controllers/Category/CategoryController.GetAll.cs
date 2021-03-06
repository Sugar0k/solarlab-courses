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
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(
            [FromServices] ICategoryService service,
            CancellationToken cancellationToken
        )
        {
            var categories = await service.GetAll(cancellationToken);
            return Ok(categories);
        }
    }
}