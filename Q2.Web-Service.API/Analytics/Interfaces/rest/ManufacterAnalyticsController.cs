using Microsoft.AspNetCore.Mvc;
using Q2.WebService.API.Analytics.Application.Internal.QueryServices;
using Quri.Teelab.Api.Teelab.Analytics.Domain.Model.Queries;
using Quri.Teelab.Api.Teelab.Analytics.Interfaces.Rest.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Quri.Teelab.Api.Teelab.Analytics.Interfaces.Rest
{
    [ApiController]
    [Route("api/analytics")]
    public class ManufacturerAnalyticsController : ControllerBase
    {
        private readonly ManufacturerAnalyticsQueryServiceImpl _manufacturerAnalyticsQueryService;

        public ManufacturerAnalyticsController(ManufacturerAnalyticsQueryServiceImpl manufacturerAnalyticsQueryService)
        {
            _manufacturerAnalyticsQueryService = manufacturerAnalyticsQueryService;
        }

        /// <summary>
        /// Obtiene métricas analíticas para un manufacturer por userId.
        /// </summary>
        /// <param name="userId">Identificador del usuario manufacturer</param>
        /// <returns>Métricas analíticas del manufacturer</returns>
        [HttpGet("manufacturer/{userId}")]
        [SwaggerOperation(
            Summary = "Get manufacturer analytics",
            Description = "Returns analytics metrics related to production and fulfillment for a manufacturer, such as total orders received, pending fulfillments, produced projects, and average fulfillment time."
        )]
        [SwaggerResponse(200, "Manufacturer analytics found and returned successfully", typeof(ManufacturerAnalyticsResource))]
        [SwaggerResponse(404, "Manufacturer analytics not found for the given userId")]
        [SwaggerResponse(400, "Invalid userId format")]
        [SwaggerResponse(500, "Internal server error")]
        public ActionResult<ManufacturerAnalyticsResource> GetManufacturerAnalytics(string userId)
        {
            if (!Guid.TryParse(userId, out var uuid))
                return BadRequest();

            var query = new GetManufacturerAnalyticsByUserIdQuery(uuid);
            var analytics = _manufacturerAnalyticsQueryService.Handle(query);
            if (analytics == null)
                return NotFound();

            // Mapeo directo usando ManufacturerAnalyticsResource
            var response = new ManufacturerAnalyticsResource(
                analytics.UserId.ToString(),
                analytics.TotalOrdersReceived,
                analytics.PendingFulfillments,
                analytics.ProducedProjects,
                analytics.AvgFulfillmentTimeDays
            );
            return Ok(response);
        }
    }
}