using System;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects
{
    public readonly struct UserId
    {
        public Guid Value { get; }

        public UserId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty");
            Value = value;
        }

        public override string ToString() => Value.ToString();

        public override bool Equals(object? obj)
        {
            return obj is UserId other && Value.Equals(other.Value);
        }

        public bool Equals(UserId other)
        {
            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(UserId left, UserId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(UserId left, UserId right)
        {
            return !(left == right);
        }
    }
}
