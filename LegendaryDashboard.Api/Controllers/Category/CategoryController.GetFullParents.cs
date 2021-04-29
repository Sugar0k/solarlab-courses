using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Category
{
    public partial class CategoryController
    {
        [HttpGet("get_full_parents")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFullParents(
            int id,
            CancellationToken cancellationToken
        )
        {
            var categories = await _categoryService.GetFullParents(id, cancellationToken);
            return Ok(categories);
        }
    }
}