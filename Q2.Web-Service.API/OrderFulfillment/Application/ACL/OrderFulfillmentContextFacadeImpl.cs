using System;
using System.Collections.Generic;
using System.Linq;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Queries;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;
using Q2.Web_Service.API.OrderFulfillment.Domain.Services;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.ACL;

namespace Q2.Web_Service.API.OrderFulfillment.Application.ACL;

/// <summary>
/// Implementation of the facade for OrderFulfillment context
/// </summary>
public class OrderFulfillmentContextFacadeImpl : IOrderFulfillmentContextFacade
{
    private readonly IManufacturerQueryService _manufacturerQueryService;
    private readonly IFulfillmentQueryService _fulfillmentQueryService;

    public OrderFulfillmentContextFacadeImpl(
        IManufacturerQueryService manufacturerQueryService,
        IFulfillmentQueryService fulfillmentQueryService)
    {
        _manufacturerQueryService = manufacturerQueryService ?? throw new ArgumentNullException(nameof(manufacturerQueryService));
        _fulfillmentQueryService = fulfillmentQueryService ?? throw new ArgumentNullException(nameof(fulfillmentQueryService));
    }

    public bool ManufacturerExists(Guid manufacturerId)
    {
        try
        {
            var manufacturers = _manufacturerQueryService.Handle(new GetAllManufacturersQuery());
            return manufacturers.Any(m => m.Id == manufacturerId);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public List<FulfillmentInfo> GetFulfillmentsByManufacturerId(Guid manufacturerId)
    {
        try
        {
            var query = new GetAllFulfillmentsByManufacturerIdQuery(new ManufacturerId(manufacturerId));
            var fulfillments = _fulfillmentQueryService.Handle(query);
            
            return fulfillments.Select(f => new FulfillmentInfo(
                f.Id,
                f.OrderId.Value,
                f.Status,
                f.ReceivedDate,
                f.ShippedDate,
                f.ManufacturerId
            )).ToList();
        }
        catch (Exception)
        {
            return new List<FulfillmentInfo>();
        }
    }
}
