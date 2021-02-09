using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LegendaryDashboard.Contracts.User.Requests
{
    public class CreateUserRequest
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
        
        [Required(ErrorMessage = "PasswordHash required ")] // необходим пароль
        public string PasswordHash { get; set; }
        
        //дата регистрации будет устанавливаться в контроллере 
        
        [Required(ErrorMessage = "Role required ")] // необходима роль
        public int RoleId { get; set; }
    }
}