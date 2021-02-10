using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace LegendaryDashboard.Application.Services.UserService.Interfaces
{
    public interface IClaimsAccessor
    {
        Task<IEnumerable<Claim>> GetCurrentClaims(CancellationToken cancellationToken);
    }
}