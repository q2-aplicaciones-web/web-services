using System;

namespace Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects
{
    /// <summary>
    /// Represents a Manufacturer identifier as a value object
    /// </summary>
    public record ManufacturerId
    {
        public Guid Value { get; }

        private ManufacturerId(Guid value)
        {
            Value = value;
        }

        public static ManufacturerId Of(Guid value)
        {
            return new ManufacturerId(value);
        }

        public static ManufacturerId Of(string value)
        {
            return new ManufacturerId(Guid.Parse(value));
        }

        public static implicit operator Guid(ManufacturerId manufacturerId) => manufacturerId.Value;
        
        public override string ToString() => Value.ToString();
    }
}
