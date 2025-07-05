using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Commands;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Services
{
    public interface IManufacturerCommandService
    {
        Manufacturer? Handle(CreateManufacturerCommand command);
    }
}
