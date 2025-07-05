using System;
using System.Collections.Generic;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Queries;
using Q2.Web_Service.API.OrderFulfillment.Domain.Services;
using Q2.Web_Service.API.OrderFulfillment.Domain.Repositories;

namespace Q2.Web_Service.API.OrderFulfillment.Application.Internal.Queryservices
{
    public class FulfillmentQueryServiceImpl : IFulfillmentQueryService
    {
        private readonly IFulfillmentRepository _fulfillmentRepository;

        public FulfillmentQueryServiceImpl(IFulfillmentRepository fulfillmentRepository)
        {
            _fulfillmentRepository = fulfillmentRepository;
        }

        public IList<Fulfillment> Handle(GetAllFulfillmentsByManufacturerIdQuery query)
        {
            // Convert ManufacturerId value object to Guid for repository
            return _fulfillmentRepository.FindByManufacturerId(query.ManufacturerId.Value);
        }

        public Fulfillment? Handle(GetFulfillmentByIdQuery query)
        {
            return _fulfillmentRepository.FindById(query.FulfillmentId);
        }
    }
}
