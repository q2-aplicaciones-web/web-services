using System;
using System.Collections.Generic;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.OrderFulfillment.Interfaces.ACL;

/// <summary>
/// Facade for accessing fulfillment information from other bounded contexts
/// </summary>
public interface IOrderFulfillmentContextFacade
{
    /// <summary>
    /// Verifies if a manufacturer exists with the specified ID
    /// </summary>
    bool ManufacturerExists(Guid manufacturerId);
    
    /// <summary>
    /// Gets all fulfillments for a specific manufacturer
    /// </summary>
    List<FulfillmentInfo> GetFulfillmentsByManufacturerId(Guid manufacturerId);
}

/// <summary>
/// DTO containing basic fulfillment information
/// </summary>
public record FulfillmentInfo(
    Guid Id,
    Guid OrderId,
    FulfillmentStatus Status,
    DateTime? ReceivedDate,
    DateTime? ShippedDate,
    Guid? ManufacturerId
);
