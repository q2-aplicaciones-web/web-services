using System;
using System.Collections.Generic;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Entities;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Repositories
{
    public interface IFulfillmentItemRepository
    {
        FulfillmentItem? FindById(FulfillmentItemId id);
        IList<FulfillmentItem> FindByFulfillmentId(Guid fulfillmentId);
        FulfillmentItem Save(FulfillmentItem item);
    }
}
