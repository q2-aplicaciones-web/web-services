using Microsoft.AspNetCore.Mvc;
using Q2.WebService.API.Analytics.Application.Internal.QueryServices;
using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Queries;
using Quri.Teelab.Api.Teelab.Analytics.Interfaces.Rest.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Quri.Teelab.Api.Teelab.Analytics.Interfaces.Rest
{
    [ApiController]
    [Route("api/v1/analytics/customer")]
    public class CustomerAnalyticsController : ControllerBase
    {
        private readonly CustomerAnalyticsQueryServiceImpl _customerAnalyticsQueryService;

        public CustomerAnalyticsController(CustomerAnalyticsQueryServiceImpl customerAnalyticsQueryService)
        {
            _customerAnalyticsQueryService = customerAnalyticsQueryService;
        }

        /// <summary>
        /// Obtiene métricas analíticas para un cliente por userId.
        /// </summary>
        /// <param name="userId">Identificador del usuario cliente</param>
        /// <returns>Métricas analíticas del cliente</returns>
        [HttpGet("{userId}")]
        [SwaggerOperation(
            Summary = "Get customer analytics",
            Description = "Returns analytics metrics related to design activities for a customer, such as total projects, blueprints, designed garments, and completed projects."
        )]
        [SwaggerResponse(200, "Customer analytics found and returned successfully", typeof(CustomerAnalyticsResource))]
        [SwaggerResponse(404, "Customer analytics not found for the given userId")]
        [SwaggerResponse(400, "Invalid userId format")]
        [SwaggerResponse(500, "Internal server error")]
        public ActionResult<CustomerAnalyticsResource> GetCustomerAnalytics(string userId)
        {
            if (!Guid.TryParse(userId, out var uuid))
                return BadRequest();

            var query = new GetCustomerAnalyticsByUserIdQuery(uuid);
            var analytics = _customerAnalyticsQueryService.Handle(query);
            if (analytics == null)
                return NotFound();

            try
            {
                var response = new CustomerAnalyticsResource(
                    analytics.UserId.ToString(),
                    analytics.TotalProjects,
                    analytics.Blueprints,
                    analytics.DesignedGarments,
                    analytics.Completed,
                    analytics.Id.ToString()
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al mapear datos de CustomerAnalytics: {ex.Message}");
            }
        }
    }
}