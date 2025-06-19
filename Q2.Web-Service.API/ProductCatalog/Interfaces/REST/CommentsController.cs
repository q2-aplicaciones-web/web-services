using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Q2.WebService.API.ProductCatalog.Domain.Services;
using Q2.WebService.API.ProductCatalog.Interfaces.REST.Resources;
using Q2.WebService.API.ProductCatalog.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Q2.WebService.API.ProductCatalog.Interfaces.REST
{
    /// <summary>    /// Comments controller for the API
    /// </summary>
    [ApiController]
    [Route("api/v1/products/{productId:guid}/comments")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Product Comments API")]
    public class CommentsController : ControllerBase
    {
        private readonly IProductCommandService _productCommandService;

        public CommentsController(IProductCommandService productCommandService)
        {
            _productCommandService = productCommandService;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Add comment",
            Description = "Add a new comment to a product",
            OperationId = "AddComment")]
        [SwaggerResponse(201, "Comment added successfully", typeof(CommentResource))]
        [SwaggerResponse(400, "Invalid comment data")]
        [SwaggerResponse(404, "Product not found")]
        public async Task<IActionResult> AddComment(
            Guid productId,
            [FromBody] CreateCommentResource resource)
        {
            var command = AddCommentCommandFromResourceAssembler.ToCommandFromResource(productId, resource);
            
            try
            {
                var commentId = await _productCommandService.Handle(command);
                
                // For simplicity, we're returning just the ID as we don't have a direct query for comments
                return CreatedAtAction(
                    nameof(AddComment), 
                    new { productId, commentId }, 
                    new { Id = commentId });
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
