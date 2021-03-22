﻿using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.CategoryService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.Category;
using LegendaryDashboard.Contracts.Contracts.Category.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Category
{
    public partial class CategoryController
    {
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> Update(
            [FromBody] CategoryDto request,
            [FromServices] ICategoryService service,
            CancellationToken cancellationToken)
        {
            await service.Update(request, cancellationToken);
            return Ok();
        }
    }
}