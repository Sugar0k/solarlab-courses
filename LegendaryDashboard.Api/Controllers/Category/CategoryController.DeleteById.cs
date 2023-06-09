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
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken
        )
        {
            await _categoryService.Delete(id, cancellationToken);
            return NoContent();
        }
    }
}