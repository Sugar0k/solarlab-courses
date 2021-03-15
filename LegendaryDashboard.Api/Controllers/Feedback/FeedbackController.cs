using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Feedback
{
    [Authorize]
    [ApiController]
    [Route("api/feedback/")]
    public partial class FeedbackController : ControllerBase
    {
        
    }
}