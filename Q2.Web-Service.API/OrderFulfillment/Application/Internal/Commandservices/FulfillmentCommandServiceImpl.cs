using System;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Commands;
using Q2.Web_Service.API.OrderFulfillment.Domain.Services;
using Q2.Web_Service.API.OrderFulfillment.Domain.Repositories;

namespace Q2.Web_Service.API.OrderFulfillment.Application.Internal.Commandservices
{
    public class FulfillmentCommandServiceImpl : IFulfillmentCommandService
    {
        private readonly IFulfillmentRepository _fulfillmentRepository;
        private readonly IManufacturerRepository _manufacturerRepository;

        public FulfillmentCommandServiceImpl(IFulfillmentRepository fulfillmentRepository, IManufacturerRepository manufacturerRepository)
        {
            _fulfillmentRepository = fulfillmentRepository;
            _manufacturerRepository = manufacturerRepository;
        }

        public Guid Handle(CreateFulfillmentCommand command)
        {
            // Buscar el manufacturer por ID
            var manufacturer = _manufacturerRepository.FindById(command.ManufacturerId.Value);
            if (manufacturer == null)
                throw new InvalidOperationException("Manufacturer not found for the given ID.");

            var fulfillment = new Fulfillment(command, manufacturer);
            _fulfillmentRepository.Save(fulfillment);
            return fulfillment.Id;
        }
    }
}
