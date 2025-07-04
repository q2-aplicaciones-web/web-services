using Microsoft.AspNetCore.Mvc;
using Q2.Web_Service.API.OrdersProcessing.Domain.Model.Queries;
using Q2.Web_Service.API.OrdersProcessing.Domain.Services;
using Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Resources;
using Q2.Web_Service.API.OrdersProcessing.Interfaces.REST.Transform;

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
}
