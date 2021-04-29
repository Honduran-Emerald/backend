using System;
using System.Collections.Generic;
using System.Text;

namespace Vitamin.Value.Domain.SeedWork
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
