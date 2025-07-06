namespace Q2.Web_Service.API.Analytics.Domain.Model.Queries;

/// <summary>
/// Query para obtener analytics de un customer específico por su user ID
/// </summary>
public record GetCustomerAnalyticsByUserIdQuery(Guid UserId);