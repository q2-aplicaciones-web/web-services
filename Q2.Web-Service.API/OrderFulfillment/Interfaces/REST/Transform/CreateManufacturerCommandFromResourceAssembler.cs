using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Commands;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Transform
{
    public static class CreateManufacturerCommandFromResourceAssembler
    {
        public static CreateManufacturerCommand ToCommandFromResource(CreateManufacturerResource resource)
        {
            return new CreateManufacturerCommand(
                resource.UserId,
                resource.Name,
                resource.Address_Street,
                resource.Address_City,
                resource.Address_Country,
                resource.Address_State,
                resource.Address_Zip
            );
        }
    }
}
