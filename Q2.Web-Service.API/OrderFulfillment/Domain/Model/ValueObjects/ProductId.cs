using System;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects
{
    public readonly struct ProductId
    {
        public Guid Value { get; }

        public ProductId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("ProductId cannot be empty");
            Value = value;
        }

        public static ProductId NewId() => new ProductId(Guid.NewGuid());

        public override string ToString() => Value.ToString();
    }
}
