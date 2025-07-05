using System.Collections.Generic;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Entities;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Queries;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Services
{
    public interface IFulfillmentItemQueryService
    {
        IList<FulfillmentItem> Handle(GetAllFulfillmentItemsByFulfillmentIdQuery query);
    }
}
