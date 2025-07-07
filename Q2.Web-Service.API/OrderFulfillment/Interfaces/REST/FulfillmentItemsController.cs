using Microsoft.AspNetCore.Mvc;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Commands;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.Queries;
using Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects;
using Q2.Web_Service.API.OrderFulfillment.Domain.Services;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Resources;
using Q2.Web_Service.API.OrderFulfillment.Interfaces.REST.Transform;

namespace Q2.Web_Service.API.OrderFulfillment.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/fulfillment-items")]
    public class FulfillmentItemsController : ControllerBase
    {
        private readonly IFulfillmentItemCommandService _fulfillmentItemCommandService;
        private readonly IFulfillmentItemQueryService _fulfillmentItemQueryService;

        public FulfillmentItemsController(
            IFulfillmentItemCommandService fulfillmentItemCommandService,
            IFulfillmentItemQueryService fulfillmentItemQueryService)
        {
            _fulfillmentItemCommandService = fulfillmentItemCommandService;
            _fulfillmentItemQueryService = fulfillmentItemQueryService;
        }

        [HttpGet("{fulfillmentId}")]
        public ActionResult<IList<FulfillmentItemResource>> GetAllFulfillmentItemsByFulfillmentId(Guid fulfillmentId)
        {
            var query = new GetAllFulfillmentItemsByFulfillmentIdQuery(fulfillmentId);
            var fulfillmentItems = _fulfillmentItemQueryService.Handle(query);
            if (fulfillmentItems == null || fulfillmentItems.Count == 0)
                return NotFound();
            var resources = new List<FulfillmentItemResource>();
            foreach (var item in fulfillmentItems)
                resources.Add(FulfillmentItemResourceFromEntityAssembler.ToResourceFromEntity(item));
            return Ok(resources);
        }

        [HttpPost("{fulfillmentItemId}/ship")]
        public ActionResult<FulfillmentItemResource> MarkFulfillmentItemAsShipped(Guid fulfillmentItemId)
        {
            var fulfillmentItemIdVO = new FulfillmentItemId(fulfillmentItemId);
            var command = new MarkFulfillmentItemAsShippedCommand(fulfillmentItemIdVO);
            try
            {
                var fulfillmentItem = _fulfillmentItemCommandService.Handle(command);
                if (fulfillmentItem == null)
                    return NotFound();
                var resource = FulfillmentItemResourceFromEntityAssembler.ToResourceFromEntity(fulfillmentItem);
                return Ok(resource);
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.Contains("does not exist", StringComparison.OrdinalIgnoreCase))
                    return NotFound();
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("{fulfillmentItemId}/receive")]
        public ActionResult<FulfillmentItemResource> MarkFulfillmentItemAsReceived(Guid fulfillmentItemId)
        {
            var fulfillmentItemIdVO = new FulfillmentItemId(fulfillmentItemId);
            var command = new MarkFulfillmentItemAsReceivedCommand(fulfillmentItemIdVO);
            try
            {
                var fulfillmentItem = _fulfillmentItemCommandService.Handle(command);
                if (fulfillmentItem == null)
                    return NotFound();
                var resource = FulfillmentItemResourceFromEntityAssembler.ToResourceFromEntity(fulfillmentItem);
                return Ok(resource);
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.Contains("does not exist", StringComparison.OrdinalIgnoreCase))
                    return NotFound();
                return BadRequest();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        [HttpPost("{fulfillmentItemId}/cancel")]
        public ActionResult<FulfillmentItemResource> CancelFulfillmentItem(Guid fulfillmentItemId)
        {
            var fulfillmentItemIdVO = new FulfillmentItemId(fulfillmentItemId);
            var command = new CancelFulfillmentItemCommand(fulfillmentItemIdVO);
            try
            {
                var fulfillmentItem = _fulfillmentItemCommandService.Handle(command);
                if (fulfillmentItem == null)
                    return NotFound();
                var resource = FulfillmentItemResourceFromEntityAssembler.ToResourceFromEntity(fulfillmentItem);
                return Ok(resource);
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.Contains("does not exist", StringComparison.OrdinalIgnoreCase))
                    return NotFound();
                return BadRequest();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
    }
}