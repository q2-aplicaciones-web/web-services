using Microsoft.AspNetCore.Mvc;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Queries;
using Q2.Web_Service.API.OrdersProcessing.Domain.Services;
using Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Resources;
using Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Q2.Web_Service.API.OrdersProcessing.Interfaces.REST;

[ApiController]
[Route("api/v1/orders")]
public class OrderProcessingController(IOrderProcessingQueryService queryService, IOrderProcessingCommandService commandService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetOrdersByUserId([FromQuery] Guid userId)
    {
        var orders = await queryService.Handle(new GetOrdersByUserIdQuery(userId));
        var resources = OrderProcessingResourceFromEntityAssembler.ToResourceFromEntity(orders);
        return Ok(resources);
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetOrderById(Guid orderId)
    {
        var order = await queryService.Handle(new GetOrderByIdQuery(orderId));
        if (order == null) return NotFound();
        var resource = OrderProcessingResourceFromEntityAssembler.ToResourceFromEntity(order);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderProcessingResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var command = CreateOrderProcessingFromEntityAssembler.ToCommand(resource);
        var orderId = await commandService.Handle(command);
        if (orderId == null) return StatusCode(500, "Error creating order");
        return CreatedAtAction(nameof(GetOrderById), new { orderId }, new { id = orderId });
    }

    [HttpPost("payment-intents")]
    [SwaggerOperation(
        Summary = "Create Stripe Payment Intent",
        Description = "Creates a Stripe Payment Intent and returns the client secret for processing payment securely"
    )]
    [SwaggerResponse(200, "Payment Intent created successfully", typeof(PaymentIntentResource))]
    [SwaggerResponse(400, "Invalid request data")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentResource resource)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = CreatePaymentIntentCommandFromResourceAssembler.CreateCommandFromResource(resource);
            var paymentIntent = await commandService.Handle(command);
            var intentResource = PaymentIntentResourceFromEntityAssembler.ToResourceFromEntity(paymentIntent);

            return Ok(intentResource);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = $"Error creating order intent: {ex.Message}" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An unexpected error occurred: {ex.Message}" });
        }
    }
}
