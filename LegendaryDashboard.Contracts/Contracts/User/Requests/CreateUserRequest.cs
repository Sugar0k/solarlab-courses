using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LegendaryDashboard.Contracts.Contracts.User.Requests
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "FirstName required ")] // необходимо имя пользователя
        public string FirstName { get; set; }
        
        [AllowNull]
        public string MiddleName { get; set; }
       
        [AllowNull]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Phone required ")] // необходим номер телефона
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "Email required ")] // необходим email???
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required ")] // необходим пароль
        public string Password { get; set; }
    }
}