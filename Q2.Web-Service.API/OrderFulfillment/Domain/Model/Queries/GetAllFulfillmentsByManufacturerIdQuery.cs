using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;
using System;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Model.Queries
{
    public record GetAllFulfillmentsByManufacturerIdQuery(ManufacturerId ManufacturerId)
    {
        public GetAllFulfillmentsByManufacturerIdQuery(string manufacturerId) : this(new ManufacturerId(Guid.Parse(manufacturerId))) { }
    }
}
