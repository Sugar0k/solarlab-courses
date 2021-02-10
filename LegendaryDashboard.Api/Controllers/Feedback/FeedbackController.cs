using LegendaryDashboard.Api.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LegendaryDashboard.Api.Controllers.Feedback
{
    [ApiController]
    [Route("api/feedback")]
    public partial class FeedbackController : ControllerBase
    {
        private readonly DashboardContext _db;
        private readonly IConfiguration _configuration;
        
        public FeedbackController(IConfiguration configuration, DashboardContext context)
        {
            _configuration = configuration;
            _db = context;
        }
        
    }
}