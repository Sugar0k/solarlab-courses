using System.ComponentModel;
using System.Linq;
using System.Security.Claims;

namespace LegendaryDashboard.Application.Services
{
    public static class ClaimsPrincipalExtensions
    {
        public static TValue GetClaimValue<TValue>(this ClaimsPrincipal principal, string type)
        {
            if (principal == null || !principal.HasClaim(x => string.Equals(x.Type, type))) return default;
            var claim = principal.Claims?.FirstOrDefault(x => x.Type == type);
            if (claim == null) return default;
            return (TValue) TypeDescriptor
                .GetConverter(typeof(TValue))
                .ConvertFrom(claim.Value);
        }
    }
}