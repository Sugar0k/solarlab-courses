using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Feedback
{
    [Authorize]
    [ApiController]
    [Route("api/feedback/")]
    [EnableCors("MyPolicy")]
    public partial class FeedbackController : ControllerBase
    {
        
    }
}