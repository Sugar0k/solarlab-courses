using System;
using System.ComponentModel;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using LegendaryDashboard.Domain.Models;
using LegendaryDashboard.Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LegendaryDashboard.Application.Services
{
    public static class ClaimsPrincipalExtensions
    {
        public static string CreateToken(User user, IOptions<JwtOptions> jwtOptions)
        {
            var expires = DateTime.UtcNow.AddMinutes(90);
            
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                new Claim("expires", expires.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var bytes = Encoding.ASCII.GetBytes(jwtOptions.Value.Key);
            
            var securityKey = new SymmetricSecurityKey(bytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Expires = expires,
                SigningCredentials = credentials,
                Subject = new ClaimsIdentity(claims),
            });
                
            return tokenHandler.WriteToken(token);
        }
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

        public static int GetUserId(IHttpContextAccessor accessor)
        {
            if (accessor.HttpContext == null) throw new Exception("Нет прав!");
            var user = accessor.HttpContext.User;
            if (user == null) throw new Exception("Нет клеймов");
            return user.GetClaimValue<int>(ClaimTypes.NameIdentifier);
        }
    }
}