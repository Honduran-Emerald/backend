using System;

namespace Vitamin.Value.Domain.SeedWork
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
