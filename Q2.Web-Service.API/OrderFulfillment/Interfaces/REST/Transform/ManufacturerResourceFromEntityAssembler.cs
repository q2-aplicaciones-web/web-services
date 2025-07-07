using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Aggregates;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Transform
{
    public static class ManufacturerResourceFromEntityAssembler
    {
        public static ManufacturerResource ToResourceFromEntity(Manufacturer entity)
        {
            return new ManufacturerResource(
                $"manufacturer-{entity.Id}",
                entity.UserId.ToString(),
                entity.Name,
                entity.Address_Street,
                entity.Address_City,
                entity.Address_Country,
                entity.Address_State,
                entity.Address_Zip,
                entity.CreatedAt,
                entity.UpdatedAt
            );
        }
    }
}
