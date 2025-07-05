using System;

namespace Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Resources
{
    public record ManufacturerResource(
        string Id,
        string UserId,
        string Name,
        string Address_Street,
        string Address_City,
        string Address_Country,
        string Address_State,
        string Address_Zip,
        DateTime CreatedAt,
        DateTime? UpdatedAt
    );
}
