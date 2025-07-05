using System;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Model.Entities
{
    public class FulfillmentItem
    {
        public FulfillmentItemId Id { get; private set; }
        public ProductId ProductId { get; private set; }
        public int Quantity { get; private set; }
        public FulfillmentItemStatus Status { get; private set; }
        public Guid FulfillmentId { get; private set; }

        public FulfillmentItem() { }

        public FulfillmentItem(FulfillmentItemId id, ProductId productId, int quantity, FulfillmentItemStatus status, Guid fulfillmentId)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            Status = status;
            FulfillmentId = fulfillmentId;
        }

        public void MarkAsShipped()
        {
            if (Status != FulfillmentItemStatus.PENDING)
                throw new InvalidOperationException("Only pending items can be shipped.");
            Status = FulfillmentItemStatus.SHIPPED;
        }

        public void MarkAsReceived()
        {
            if (Status != FulfillmentItemStatus.SHIPPED)
                throw new InvalidOperationException("Only shipped items can be received.");
            Status = FulfillmentItemStatus.RECEIVED;
        }

        public void MarkAsCancelled()
        {
            if (Status != FulfillmentItemStatus.PENDING)
                throw new InvalidOperationException("Only pending items can be cancelled.");
            Status = FulfillmentItemStatus.CANCELLED;
        }
    }
}

