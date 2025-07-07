using Microsoft.AspNetCore.Mvc;
using Q2.Web_Service.API.Analytics.Application.Internal.Queryservices;
using Q2.Web_Service.API.Analytics.Domain.Model.Queries;
using Q2.Web_Service.API.Analytics.Interfaces.Rest.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Q2.Web_Service.API.Analytics.Interfaces.Rest
{
    [ApiController]
    [Route("api/v1/analytics")]
    public class CustomerAnalyticsController : ControllerBase
    {
        private readonly CustomerAnalyticsRealTimeQueryServiceImpl _customerAnalyticsQueryService;

        public CustomerAnalyticsController(CustomerAnalyticsRealTimeQueryServiceImpl customerAnalyticsQueryService)
        {
            _customerAnalyticsQueryService = customerAnalyticsQueryService;
        }

        /// <summary>
        /// Obtiene métricas de actividad para un cliente específico.
        /// </summary>
        /// <param name="userId">Identificador único del usuario/cliente</param>
        /// <returns>Métricas analíticas del cliente</returns>
        [HttpGet("customer-kpis/{userId}")]
        [SwaggerOperation(
            Summary = "Get customer KPIs",
            Description = "Returns analytics metrics related to design activities for a customer, including total projects, blueprints, designed garments, and completed orders."
        )]
        [SwaggerResponse(200, "Customer analytics found and returned successfully", typeof(CustomerAnalyticsResource))]
        [SwaggerResponse(404, "Customer analytics not found")]
        [SwaggerResponse(400, "Invalid userId format")]
        [SwaggerResponse(500, "Internal server error")]
        public ActionResult<CustomerAnalyticsResource> GetCustomerKpis(string userId)
        {
            if (!Guid.TryParse(userId, out var uuid))
                return BadRequest("Invalid userId format");

            var query = new GetCustomerAnalyticsByUserIdQuery(uuid);
            
            try
            {
                var analytics = _customerAnalyticsQueryService.Handle(query);
                if (analytics == null)
                    return NotFound("Customer analytics not found");

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
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
