using System;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects
{
    public readonly struct ManufacturerId
    {
        public Guid Value { get; }

        public ManufacturerId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Manufacturer ID cannot be empty");
            Value = value;
        }

        public static ManufacturerId NewId() => new ManufacturerId(Guid.NewGuid());

        public override string ToString() => Value.ToString();
    }
}
