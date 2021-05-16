namespace Vitamin.Value.Domain.SeedWork
{
    public abstract class ValueObject
    {
        public override abstract bool Equals(object obj);

        public static bool operator ==(ValueObject left, ValueObject right)
            => ReferenceEquals(left, null) || ReferenceEquals(right, null)
                ? ReferenceEquals(left, right)
                : left.Equals(right);

        public static bool operator !=(ValueObject left, ValueObject right)
            => !(left == right);
    }
}
