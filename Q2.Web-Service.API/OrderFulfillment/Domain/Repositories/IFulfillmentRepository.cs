using System;
using System.Collections.Generic;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Repositories
{
    public interface IFulfillmentRepository
    {
        Fulfillment? FindById(Guid id);
        IList<Fulfillment> FindByManufacturerId(Guid manufacturerId);
        Fulfillment? FindByOrderId(Guid orderId);
        Fulfillment Save(Fulfillment fulfillment);
    }
}
