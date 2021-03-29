using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Category
{
    [ApiController]
    [Route("api/category/")]
    [EnableCors("MyPolicy")]
    public partial class CategoryController : ControllerBase
    {
        
    }
}