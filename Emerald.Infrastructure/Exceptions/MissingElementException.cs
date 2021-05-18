using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Vitamin.Value.Domain.SeedWork;

namespace Emerald.Infrastructure.Exceptions
{
    public class MissingElementException : DomainException
    {
        public MissingElementException() : base("Missing element")
        {
        }

        public MissingElementException(string message) : base(message)
        {
        }
    }
}
