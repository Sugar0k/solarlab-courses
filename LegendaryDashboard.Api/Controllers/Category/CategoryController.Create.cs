using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.CategoryService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.Category.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Category
{
    public partial class CategoryController
    {
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(
            [FromBody] CreateCategoryRequest request,
            [FromServices] ICategoryService service,
            CancellationToken cancellationToken)
        {
            await service.Save(request, cancellationToken);
            return Ok();
        }
    }
}