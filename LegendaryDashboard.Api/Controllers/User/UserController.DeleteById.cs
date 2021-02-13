using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.User
{
    
    public partial class UserController
    {
        //TODO: Добавить Authorize(Roles = "admin")
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] IUserService service,
            CancellationToken cancellationToken
        )
        {
            await service.Delete(id, cancellationToken);
            return NoContent();
        }
    }
}