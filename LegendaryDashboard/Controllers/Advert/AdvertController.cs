using Microsoft.AspNetCore.Mvc;
using LegendaryDashboard.DbContext;

namespace LegendaryDashboard.Controllers.Advert
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
