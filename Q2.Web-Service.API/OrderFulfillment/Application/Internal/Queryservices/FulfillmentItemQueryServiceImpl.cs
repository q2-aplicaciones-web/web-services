using System;
using System.Collections.Generic;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Entities;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Queries;
using Q2.Web_Service.API.OrderFulfillment.Domain.Services;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;
using Q2.Web_Service.API.OrderFulfillment.Domain.Repositories;

namespace Q2.Web_Service.API.OrderFulfillment.Application.Internal.Queryservices
{
    public class FulfillmentItemQueryServiceImpl : IFulfillmentItemQueryService
    {
        private readonly IFulfillmentItemRepository _fulfillmentItemRepository;

        public FulfillmentItemQueryServiceImpl(IFulfillmentItemRepository fulfillmentItemRepository)
        {
            _fulfillmentItemRepository = fulfillmentItemRepository;
        }

        public IList<FulfillmentItem> Handle(GetAllFulfillmentItemsByFulfillmentIdQuery query)
        {
            return _fulfillmentItemRepository.FindByFulfillmentId(query.FulfillmentId);
        }
    }
}
