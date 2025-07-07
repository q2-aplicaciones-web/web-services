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
        private readonly Q2.Web_Service.API.OrdersProcessing.Domain.Repositories.IOrderProcessingRepository _orderProcessingRepository;
        private readonly IFulfillmentItemRepository _fulfillmentItemRepository;

        public FulfillmentCommandServiceImpl(
            IFulfillmentRepository fulfillmentRepository,
            IManufacturerRepository manufacturerRepository,
            Q2.Web_Service.API.OrdersProcessing.Domain.Repositories.IOrderProcessingRepository orderProcessingRepository,
            IFulfillmentItemRepository fulfillmentItemRepository)
        {
            _fulfillmentRepository = fulfillmentRepository;
            _manufacturerRepository = manufacturerRepository;
            _orderProcessingRepository = orderProcessingRepository;
            _fulfillmentItemRepository = fulfillmentItemRepository;
        }

        public Guid Handle(CreateFulfillmentCommand command)
        {
            // Buscar el manufacturer por ID
            var manufacturer = _manufacturerRepository.FindById(command.ManufacturerId.Value);
            if (manufacturer == null)
                throw new InvalidOperationException("Manufacturer not found for the given ID.");

            // Buscar la orden por ID
            var orderTask = _orderProcessingRepository.GetOrderByIdAsync(command.OrderId.Value);
            orderTask.Wait();
            var order = orderTask.Result;
            if (order == null)
                throw new InvalidOperationException("Order not found for the given ID.");

            var fulfillment = new Fulfillment(command, manufacturer);
            _fulfillmentRepository.Save(fulfillment);

            // Crear FulfillmentItems para cada item de la orden
            foreach (var item in order.Items)
            {
                var fulfillmentItem = fulfillment.CreateItem(
                    new Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects.ProductId(item.ProductId),
                    item.Quantity,
                    Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects.FulfillmentItemStatus.PENDING
                );
                _fulfillmentItemRepository.Save(fulfillmentItem);
            }

            return fulfillment.Id;
        }
        public void MarkAsShipped(Guid fulfillmentId)
        {
            var fulfillment = _fulfillmentRepository.FindById(fulfillmentId);
            if (fulfillment == null)
                throw new InvalidOperationException("Fulfillment not found.");
            fulfillment.MarkAsShipped();
            _fulfillmentRepository.Save(fulfillment);
        }

        public void MarkAsReceived(Guid fulfillmentId)
        {
            var fulfillment = _fulfillmentRepository.FindById(fulfillmentId);
            if (fulfillment == null)
                throw new InvalidOperationException("Fulfillment not found.");
            fulfillment.MarkAsReceived();
            _fulfillmentRepository.Save(fulfillment);
        }
    }
}
