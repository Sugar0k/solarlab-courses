using LegendaryDashboard.Api.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LegendaryDashboard.Api.Controllers.Image
{
    [ApiController]
    [Route("api/image/")]
    public partial class ImageController: ControllerBase
    {
        private readonly DashboardContext _db;
        private readonly IConfiguration _configuration;

        public ImageController(IConfiguration configuration, DashboardContext context) 
        {
            _configuration = configuration;
            _db = context;
        }
    }
}