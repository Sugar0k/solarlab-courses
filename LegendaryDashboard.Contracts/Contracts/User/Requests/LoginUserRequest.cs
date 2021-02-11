using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LegendaryDashboard.Contracts.Contracts.User.Requests
{
    public class LoginUserRequest
    {
        [Required(ErrorMessage = "Email required ")] // необходим email
        public string Email { get; set; }
        
        [Required(ErrorMessage = "PasswordHash required ")] // необходим пароль
        public string PasswordHash { get; set; }
    }
}