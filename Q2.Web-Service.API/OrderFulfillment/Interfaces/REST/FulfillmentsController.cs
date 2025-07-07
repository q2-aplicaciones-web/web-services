using Microsoft.AspNetCore.Mvc;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Queries;
using Q2.Web_Service.API.OrderFulfillment.Domain.Services;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Resources;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Transform;

namespace Q2.Web_Service.API.OrderFulfillment.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/fulfillments")]
    public class FulfillmentsController : ControllerBase
    {
        private readonly IFulfillmentCommandService _fulfillmentCommandService;
        private readonly IFulfillmentQueryService _fulfillmentQueryService;

        public FulfillmentsController(
            IFulfillmentCommandService fulfillmentCommandService,
            IFulfillmentQueryService fulfillmentQueryService)
        {
            _fulfillmentCommandService = fulfillmentCommandService;
            _fulfillmentQueryService = fulfillmentQueryService;
        }

        [HttpGet]
        public ActionResult<IList<FulfillmentResource>> GetAllFulfillments([FromQuery] Guid? manufacturerId)
        {
            if (manufacturerId.HasValue)
            {
                var query = new GetAllFulfillmentsByManufacturerIdQuery(new Domain.Model.ValueObjects.ManufacturerId(manufacturerId.Value));
                var fulfillments = _fulfillmentQueryService.Handle(query);
                if (fulfillments == null || fulfillments.Count == 0)
                    return NotFound();
                var resources = new List<FulfillmentResource>();
                foreach (var f in fulfillments)
                    resources.Add(FulfillmentResourceFromEntityAssembler.ToResourceFromEntity(f));
                return Ok(resources);
            }
            // TODO: Implement GetAllFulfillmentsQuery for when no manufacturerId is provided
            return NotFound();
        }

        [HttpGet("{fulfillmentId}")]
        public ActionResult<FulfillmentResource> GetFulfillmentById(Guid fulfillmentId)
        {
            var query = new GetFulfillmentByIdQuery(fulfillmentId);
            var fulfillment = _fulfillmentQueryService.Handle(query);
            if (fulfillment == null)
                return NotFound();
            var resource = FulfillmentResourceFromEntityAssembler.ToResourceFromEntity(fulfillment);
            return Ok(resource);
        }

        [HttpPost]
        public ActionResult<FulfillmentResource> CreateFulfillment([FromBody] CreateFulfillmentResource resource)
        {
            var command = CreateFulfillmentCommandFromResourceAssembler.ToCommandFromResource(resource);
            var fulfillmentId = _fulfillmentCommandService.Handle(command);
            if (fulfillmentId == Guid.Empty)
                return BadRequest();
            var query = new GetFulfillmentByIdQuery(fulfillmentId);
            var fulfillment = _fulfillmentQueryService.Handle(query);
            if (fulfillment == null)
                return NotFound();
            var fulfillmentResource = FulfillmentResourceFromEntityAssembler.ToResourceFromEntity(fulfillment);
            return CreatedAtAction(nameof(GetFulfillmentById), new { fulfillmentId = fulfillmentResource.Id }, fulfillmentResource);
        }
        [HttpPost("{fulfillmentId}/ship")]
        public IActionResult MarkAsShipped(Guid fulfillmentId)
        {
            try
            {
                _fulfillmentCommandService.MarkAsShipped(fulfillmentId);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("{fulfillmentId}/receive")]
        public IActionResult MarkAsReceived(Guid fulfillmentId)
        {
            try
            {
                _fulfillmentCommandService.MarkAsReceived(fulfillmentId);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
