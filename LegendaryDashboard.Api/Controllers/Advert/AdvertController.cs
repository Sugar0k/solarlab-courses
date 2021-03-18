using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/advert/")]
    public partial class AdvertController : ControllerBase
    {
        
    }
}