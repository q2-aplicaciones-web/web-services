namespace Q2.Web_Service.API.IAM.Domain.Model.Queries;

/**
 * <summary>
 *     The get user by id query
 * </summary>
 * <remarks>
 *     This query object includes the user id to search
 * </remarks>
 */
public record GetUserByIdQuery(Guid Id);