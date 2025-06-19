using System;

namespace Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects
{
    /// <summary>
    /// Represents a rating value object that ensures the rating is between 0 and 5
    /// </summary>
    public record Rating
    {
        public double Value { get; }

        private Rating(double value)
        {
            if (value < 0 || value > 5)
                throw new ArgumentException("Rating must be between 0 and 5", nameof(value));
            
            Value = value;
        }

        public static Rating Of(double value)
        {
            return new Rating(value);
        }

        public static implicit operator double(Rating rating) => rating.Value;
        
        public override string ToString() => Value.ToString("F1");
    }
}
