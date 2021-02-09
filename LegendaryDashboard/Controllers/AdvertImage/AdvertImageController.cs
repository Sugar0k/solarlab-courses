using LegendaryDashboard.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LegendaryDashboard.Controllers.AdvertImage
{
    [ApiController]
    [Route("api/advertImage/")]
    public partial class AdvertImageController: ControllerBase
    {
        private readonly DashboardContext _db;
        private readonly IConfiguration _configuration;

        public AdvertImageController(IConfiguration configuration, DashboardContext context) 
        {
            _configuration = configuration;
            _db = context;
        }
        
    }
    
}