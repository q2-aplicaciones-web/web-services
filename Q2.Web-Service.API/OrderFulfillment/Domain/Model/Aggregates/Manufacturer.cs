using System;
using System.Collections.Generic;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;
using Q2.Web_Service.API.Shared.Domain.Model.Aggregates;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates
{
    public class Manufacturer : AuditableAbstractAggregateRoot<Manufacturer>
    {
        public UserId UserId { get; private set; }
        public Guid UserIdGuid => UserId.Value;
        public string Name { get; private set; } = string.Empty;
        // Flattened address fields
        public string Address_Street { get; private set; } = string.Empty;
        public string Address_City { get; private set; } = string.Empty;
        public string Address_Country { get; private set; } = string.Empty;
        public string Address_State { get; private set; } = string.Empty;
        public string Address_Zip { get; private set; } = string.Empty;
        private readonly List<Fulfillment> _fulfillments = new();
        public IReadOnlyList<Fulfillment> Fulfillments => _fulfillments.AsReadOnly();

        public Manufacturer() { }

        public Manufacturer(string userId, string name, string addressStreet, string addressCity, string addressCountry, string addressState, string addressZip)
        {
            UserId = new UserId(Guid.Parse(userId));
            Name = name;
            Address_Street = addressStreet;
            Address_City = addressCity;
            Address_Country = addressCountry;
            Address_State = addressState;
            Address_Zip = addressZip;
        }

        public void UpdateInformation(string name, string addressStreet, string addressCity, string addressCountry, string addressState, string addressZip)
        {
            Name = name;
            Address_Street = addressStreet;
            Address_City = addressCity;
            Address_Country = addressCountry;
            Address_State = addressState;
            Address_Zip = addressZip;
        }

        public void AddFulfillment(Fulfillment fulfillment)
        {
            _fulfillments.Add(fulfillment);
        }

        public void RemoveFulfillment(Fulfillment fulfillment)
        {
            _fulfillments.Remove(fulfillment);
        }

        public Fulfillment CreateFulfillment(OrderId orderId, FulfillmentStatus status)
        {
            // Usa el constructor de 5 parámetros para evitar ambigüedad
            var fulfillment = new Fulfillment(orderId, status, null, null, this);
            return fulfillment;
        }
    }
}
