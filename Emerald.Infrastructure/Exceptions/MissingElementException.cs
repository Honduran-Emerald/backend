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
