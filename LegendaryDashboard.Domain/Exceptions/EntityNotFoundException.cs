using System;

namespace LegendaryDashboard.Domain.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}