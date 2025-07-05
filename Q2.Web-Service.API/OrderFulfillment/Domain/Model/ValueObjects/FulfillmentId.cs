using System;

namespace Q2.Teelab.Api.Teelab.OrderFulfillment.Domain.Model.ValueObjects
{
    public readonly struct FulfillmentId
    {
        public Guid Value { get; }

        public FulfillmentId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Fulfillment ID cannot be empty");
            Value = value;
        }

        public static FulfillmentId NewId() => new FulfillmentId(Guid.NewGuid());

        public override string ToString() => Value.ToString();
    }
}
