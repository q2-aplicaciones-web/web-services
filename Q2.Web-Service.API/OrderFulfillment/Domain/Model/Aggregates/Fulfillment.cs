using System;
using System.Collections.Generic;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Commands;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Entities;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;
using Q2.Web_Service.API.Shared.Domain.Model.Aggregates;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates
{
    public class Fulfillment : AuditableAbstractAggregateRoot<Fulfillment>
    {
        public OrderId OrderId { get; private set; }
        public FulfillmentStatus Status { get; private set; }
        public DateTime? ReceivedDate { get; private set; }
        public DateTime? ShippedDate { get; private set; }
        public Guid? ManufacturerId { get; private set; }
        public Manufacturer? Manufacturer { get; private set; }
        private readonly List<FulfillmentItem> _items = new();
        public IReadOnlyList<FulfillmentItem> Items => _items.AsReadOnly();

        public Fulfillment()
        {
            // Default constructor for EF Core
            Manufacturer = null;
        }

        public Fulfillment(OrderId orderId, FulfillmentStatus status, DateTime? receivedDate, DateTime? shippedDate, Manufacturer manufacturer)
        {
            OrderId = orderId;
            Status = status;
            ReceivedDate = receivedDate;
            ShippedDate = shippedDate;
            Manufacturer = manufacturer;
            ManufacturerId = manufacturer?.Id;
            manufacturer?.AddFulfillment(this);
        }

        public Fulfillment(CreateFulfillmentCommand command, Manufacturer manufacturer)
        {
            OrderId = command.OrderId;
            Status = FulfillmentStatus.PENDING;
            ReceivedDate = null;
            ShippedDate = null;
            Manufacturer = manufacturer;
            ManufacturerId = manufacturer?.Id;
            manufacturer?.AddFulfillment(this);
        }

        public Fulfillment(CreateFulfillmentCommand command)
        {
            OrderId = command.OrderId;
            Status = FulfillmentStatus.PENDING;
            ReceivedDate = null;
            ShippedDate = null;
            Manufacturer = null;
            ManufacturerId = null;
        }

        public void UpdateStatus(FulfillmentStatus status)
        {
            Status = status;
        }

        public void UpdateReceivedDate(DateTime? receivedDate)
        {
            ReceivedDate = receivedDate;
        }

        public void UpdateShippedDate(DateTime? shippedDate)
        {
            ShippedDate = shippedDate;
        }

        public void AddItem(FulfillmentItem item)
        {
            _items.Add(item);
        }

        public void RemoveItem(FulfillmentItem item)
        {
            _items.Remove(item);
        }

        public FulfillmentItem CreateItem(ProductId productId, int quantity, FulfillmentItemStatus status)
        {
            var item = new FulfillmentItem(FulfillmentItemId.NewId(), productId, quantity, status, this.Id);
            AddItem(item);
            return item;
        }

        public Guid? GetManufacturerId()
        {
            return Manufacturer?.Id;
        }
    }
}
