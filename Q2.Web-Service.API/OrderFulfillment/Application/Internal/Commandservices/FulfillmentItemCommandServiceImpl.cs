using System;
using System.Collections.Generic;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Commands;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Entities;
using Q2.Web_Service.API.OrderFulfillment.Domain.Services;
using Q2.Web_Service.API.OrderFulfillment.Domain.Repositories;

namespace Q2.Teelab.Api.Teelab.OrderFulfillment.Application.Internal.Commandservices
{
    public class FulfillmentItemCommandServiceImpl : IFulfillmentItemCommandService
    {
        private readonly IFulfillmentItemRepository _fulfillmentItemRepository;

        public FulfillmentItemCommandServiceImpl(IFulfillmentItemRepository fulfillmentItemRepository)
        {
            _fulfillmentItemRepository = fulfillmentItemRepository;
        }

        public FulfillmentItem? Handle(MarkFulfillmentItemAsShippedCommand command)
        {
            var fulfillmentItem = _fulfillmentItemRepository.FindById(command.FulfillmentItemId);
            if (fulfillmentItem == null)
                throw new ArgumentException("FulfillmentItem does not exist");

            fulfillmentItem.MarkAsShipped();
            var updatedFulfillmentItem = _fulfillmentItemRepository.Save(fulfillmentItem);
            return updatedFulfillmentItem;
        }

        public FulfillmentItem? Handle(MarkFulfillmentItemAsReceivedCommand command)
        {
            var fulfillmentItem = _fulfillmentItemRepository.FindById(command.FulfillmentItemId);
            if (fulfillmentItem == null)
                throw new ArgumentException("FulfillmentItem does not exist");

            fulfillmentItem.MarkAsReceived();
            var updatedFulfillmentItem = _fulfillmentItemRepository.Save(fulfillmentItem);
            return updatedFulfillmentItem;
        }

        public FulfillmentItem? Handle(CancelFulfillmentItemCommand command)
        {
            var fulfillmentItem = _fulfillmentItemRepository.FindById(command.FulfillmentItemId);
            if (fulfillmentItem == null)
                throw new ArgumentException("FulfillmentItem does not exist");

            fulfillmentItem.MarkAsCancelled();
            var updatedFulfillmentItem = _fulfillmentItemRepository.Save(fulfillmentItem);
            return updatedFulfillmentItem;
        }
    }
}
