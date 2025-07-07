using System;
using Microsoft.AspNetCore.Mvc;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Commands;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Queries;
using Q2.Web_Service.API.ProductCatalog.Domain.Services;
using Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources;
using Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Transform;
using Q2.Web_Service.API.DesignLab.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.ProductCatalog.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/products")]
    [Produces("application/json")]
    public class ProductLikesController : ControllerBase
    {
        private readonly IProductLikeService _productLikeService;

        public ProductLikesController(IProductLikeService productLikeService)
        {
            _productLikeService = productLikeService;
        }

        [HttpPost("{productId}/likes/{userId}")]
        [ProducesResponseType(typeof(ProductLikeStatusResource), 201)]
        [ProducesResponseType(typeof(ProductLikeStatusResource), 409)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LikeProduct(Guid productId, Guid userId)
        {
            try
            {
                var checkQuery = new CheckIfUserLikedProductQuery(productId, userId);
                var isAlreadyLiked = await _productLikeService.Handle(checkQuery);
                if (isAlreadyLiked)
                {
                    var resource = ProductLikeStatusResourceAssembler.ToAlreadyLikedResource(productId, userId);
                    return Conflict(resource);
                }
                var command = new LikeProductCommand(productId, userId);
                await _productLikeService.Handle(command);
                var successResource = ProductLikeStatusResourceAssembler.ToLikeSuccessResource(productId, userId);
                return StatusCode(201, successResource);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                var errorResource = new ErrorResource($"Internal server error: {e.Message}", "Internal Server Error", 500, DateTime.UtcNow);
                return StatusCode(500, errorResource);
            }
        }

        [HttpDelete("{productId}/likes/{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UnlikeProduct(Guid productId, Guid userId)
        {
            try
            {
                var checkQuery = new CheckIfUserLikedProductQuery(productId, userId);
                var isLiked = await _productLikeService.Handle(checkQuery);
                if (!isLiked)
                {
                    return NotFound();
                }
                var command = new UnlikeProductCommand(productId, userId);
                await _productLikeService.Handle(command);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{productId}/likes/count")]
        [ProducesResponseType(typeof(ProductLikeCountResource), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ProductLikeCountResource>> GetLikeCount(Guid productId)
        {
            try
            {
                var query = new GetLikeCountByProductQuery(productId);
                var likeCount = await _productLikeService.Handle(query);
                var resource = ProductLikeCountResourceAssembler.ToResourceFromCount(likeCount);
                return Ok(resource);
            }
            catch (Exception)
            {
                var resource = ProductLikeCountResourceAssembler.ToResourceFromCount(0L);
                return Ok(resource);
            }
        }

        [HttpGet("{productId}/likes/{userId}")]
        [ProducesResponseType(typeof(ProductUserLikeStatusResource), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ProductUserLikeStatusResource>> CheckIfUserLiked(Guid productId, Guid userId)
        {
            try
            {
                var query = new CheckIfUserLikedProductQuery(productId, userId);
                var isLiked = await _productLikeService.Handle(query);
                var resource = ProductUserLikeStatusResourceAssembler.ToResourceFromStatus(isLiked);
                return Ok(resource);
            }
            catch (Exception)
            {
                var resource = ProductUserLikeStatusResourceAssembler.ToResourceFromStatus(false);
                return Ok(resource);
            }
        }
    }
}
