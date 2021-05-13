using System;

namespace LegendaryDashboard.Domain.Exceptions
{
    public class UserReadableException: Exception
    {
        public UserReadableException(string message) : base(message)
        {
        }  
    }
}