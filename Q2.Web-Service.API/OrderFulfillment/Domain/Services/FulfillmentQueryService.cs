using System.Collections.Generic;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Queries;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Services
{
    public interface IFulfillmentQueryService
    {
        IList<Fulfillment> Handle(GetAllFulfillmentsByManufacturerIdQuery query);
        Fulfillment? Handle(GetFulfillmentByIdQuery query);
    }
}
