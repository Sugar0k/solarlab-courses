using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/advert/")]
    [EnableCors("MyPolicy")]
    public partial class AdvertController : ControllerBase
    {
        
    }
}