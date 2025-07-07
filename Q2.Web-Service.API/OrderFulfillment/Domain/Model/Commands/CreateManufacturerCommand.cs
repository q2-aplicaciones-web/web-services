namespace Q2.Web_Service.API.OrderFulfillment.Domain.Model.Commands
{
    public record CreateManufacturerCommand(
        string UserId,
        string Name,
        string Address_Street,
        string Address_City,
        string Address_Country,
        string Address_State,
        string Address_Zip
    );
}
