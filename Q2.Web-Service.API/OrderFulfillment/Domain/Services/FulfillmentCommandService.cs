using System;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Commands;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Services
{
    public interface IFulfillmentCommandService
    {
        Guid Handle(CreateFulfillmentCommand command);
    }
}
