using Microsoft.AspNetCore.Mvc;
using Q2.Web_Service.API.Analytics.Application.Internal.Queryservices;
using Q2.Web_Service.API.Analytics.Domain.Model.Queries;
using Q2.Web_Service.API.Analytics.Interfaces.Rest.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace Q2.Web_Service.API.Analytics.Interfaces.Rest
{
    [ApiController]
    [Route("api/v1/analytics")]
    public class ManufacturerAnalyticsController : ControllerBase
    {
        private readonly ManufacturerAnalyticsRealTimeQueryServiceImpl _manufacturerAnalyticsQueryService;

        public ManufacturerAnalyticsController(ManufacturerAnalyticsRealTimeQueryServiceImpl manufacturerAnalyticsQueryService)
        {
            _manufacturerAnalyticsQueryService = manufacturerAnalyticsQueryService;
        }

        /// <summary>
        /// Obtiene métricas de rendimiento para un fabricante específico.
        /// </summary>
        /// <param name="manufacturerId">Identificador único del fabricante</param>
        /// <returns>Métricas analíticas del manufacturer</returns>
        [HttpGet("manufacturer-kpis/{manufacturerId}")]
        [SwaggerOperation(
            Summary = "Get manufacturer KPIs",
            Description = "Returns analytics metrics related to production and fulfillment for a manufacturer, including total orders received, pending fulfillments, produced projects, and average fulfillment time."
        )]
        [SwaggerResponse(200, "Manufacturer analytics found and returned successfully", typeof(ManufacturerAnalyticsResource))]
        [SwaggerResponse(404, "Manufacturer not found")]
        [SwaggerResponse(400, "Invalid manufacturerId format")]
        [SwaggerResponse(500, "Internal server error")]
        public ActionResult<ManufacturerAnalyticsResource> GetManufacturerKpis(string manufacturerId)
        {
            if (!Guid.TryParse(manufacturerId, out var uuid))
                return BadRequest("Invalid manufacturerId format");

            var query = new GetManufacturerAnalyticsByManufacturerIdQuery(uuid);
            
            try
            {
                var analytics = _manufacturerAnalyticsQueryService.Handle(query);
                if (analytics == null)
                    return NotFound("Manufacturer not found");

                var response = new ManufacturerAnalyticsResource(
                    analytics.ManufacturerId.ToString(),
                    analytics.TotalOrdersReceived,
                    analytics.PendingFulfillments,
                    analytics.ProducedProjects,
                    analytics.AvgFulfillmentTimeDays
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
