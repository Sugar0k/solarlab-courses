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
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            int offset, int limit,
            [FromServices] ICategoryService service,
            CancellationToken cancellationToken
        )
        {
            return Ok(await service.GetPaged(offset, limit, cancellationToken));
        }
    }
}