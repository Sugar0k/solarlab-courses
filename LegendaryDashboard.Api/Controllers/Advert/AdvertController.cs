using LegendaryDashboard.Api.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    [ApiController]
    [Route("api/advert/")]
    public partial class AdvertController : ControllerBase
    {
        private readonly DashboardContext _db;

        public AdvertController(DashboardContext context)
        {
            _db = context;
        }
    }
}
