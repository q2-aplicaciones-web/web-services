using System;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Commands;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Transform
{
    public static class CreateFulfillmentCommandFromResourceAssembler
    {
        public static CreateFulfillmentCommand ToCommandFromResource(CreateFulfillmentResource resource)
        {
            return new CreateFulfillmentCommand(
                new OrderId(Guid.Parse(resource.OrderId)),
                new ManufacturerId(Guid.Parse(resource.ManufacturerId))
            );
        }
    }
}
