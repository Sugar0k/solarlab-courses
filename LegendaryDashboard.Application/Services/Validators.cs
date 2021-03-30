using System.Text.RegularExpressions;

namespace LegendaryDashboard.Application.Services
{
    public static class Validators
    {
        public static bool PhoneChecker(string phoneNumber)
        {
            Match phoneValidation  = Regex.
                Match(phoneNumber,
                    @"^(?:\(?)(?<AreaCode>\d{3})(?:[\).\s]?)(?<Prefix>\d{3})(?:[-\.\s]?)(?<Suffix>\d{4})(?!\d)");

            return phoneValidation.Success;
        }

        public static bool EmailChecker(string email)
        {
            Match emailValidation = Regex.
                Match(email,
                    "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}");
            
            return emailValidation.Success;
        }
    }
}