namespace Q2.Web_Service.API.Analytics.Domain.Model.Queries;

/// <summary>
/// Query para obtener analytics de un manufacturer específico por su ID
/// </summary>
public record GetManufacturerAnalyticsByManufacturerIdQuery(Guid ManufacturerId);
