using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace LegendaryDashboard.Application.Services
{
    public static class ClaimsPrincipalExtensions
    {
        
        private static TValue GetClaimValue<TValue>(this ClaimsPrincipal principal, string type)
        {
            if (principal == null || !principal.HasClaim(x => string.Equals(x.Type, type))) return default;
            var claim = principal.Claims?.FirstOrDefault(x => x.Type == type);
            if (claim == null) return default;
            return (TValue) TypeDescriptor
                .GetConverter(typeof(TValue))
                .ConvertFrom(claim.Value);
        }

        public static bool IsAdminOrOwner(IHttpContextAccessor accessor, int id)
        {
            if (accessor.HttpContext == null) throw new Exception("Нет прав!");
            var user = accessor.HttpContext.User;
            if (user == null) throw new Exception("Нет клеймов");
            if (id == user.GetClaimValue<int>(ClaimTypes.NameIdentifier) || 
                "Admin".Equals(user.GetClaimValue<string>(ClaimTypes.Role)))
            {
                return true;
            }
            return false;
        }
    }
}