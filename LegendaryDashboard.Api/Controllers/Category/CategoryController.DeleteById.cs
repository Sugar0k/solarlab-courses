using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.CategoryService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Category
{
    
    public partial class CategoryController
    {
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] ICategoryService service,
            CancellationToken cancellationToken
        )
        {
            await service.Delete(id, cancellationToken);
            return NoContent();
        }
    }
}