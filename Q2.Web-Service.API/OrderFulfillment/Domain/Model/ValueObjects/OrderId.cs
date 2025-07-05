using System;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects
{
    public readonly struct OrderId
    {
        public Guid Value { get; }

        public OrderId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Order ID cannot be empty");
            Value = value;
        }

        public override string ToString() => Value.ToString();
    }
}
