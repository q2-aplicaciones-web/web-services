using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Queries;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Commands;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;
using Q2.Web_Service.API.ProductCatalog.Domain.Services;
using Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources;
using Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Transform;
using Q2.Web_Service.API.ProductCatalog.Interfaces.ACL;

namespace Q2.Web_Service.API.ProductCatalog.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/products")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductCommandService _productCommandService;
        private readonly IProductQueryService _productQueryService;
        private readonly IProjectContextFacade _projectContextFacade;

        public ProductsController(
            IProductCommandService productCommandService,
            IProductQueryService productQueryService,
            IProjectContextFacade projectContextFacade)
        {
            _productCommandService = productCommandService;
            _productQueryService = productQueryService;
            _projectContextFacade = projectContextFacade;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProductResource>), 200)]
        public async Task<ActionResult<List<ProductResource>>> GetAllProducts([FromQuery] string? projectId)
        {
            List<ProductResource> productResources;
            if (!string.IsNullOrWhiteSpace(projectId))
            {
                var products = await _productQueryService.Handle(new GetProductsByProjectIdQuery(ProjectId.Of(projectId)));
                productResources = products.ConvertAll(ProductResourceFromEntityAssembler.ToResourceFromEntity);
            }
            else
            {
                var products = await _productQueryService.Handle(new GetAllProductsQuery());
                productResources = products.ConvertAll(ProductResourceFromEntityAssembler.ToResourceFromEntity);
            }
            return Ok(productResources);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(ProductResource), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ProductResource>> GetProductById(Guid productId)
        {
            var product = await _productQueryService.Handle(new GetProductByIdQuery(productId));
            if (product == null)
                return NotFound();
            var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
            return Ok(productResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductResource), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ProductResource>> CreateProduct([FromBody] CreateProductResource resource)
        {
            try
            {
                if (resource == null)
                    return BadRequest("Resource is null");
                var createProductCommand = CreateProductCommandFromResourceAssembler.ToCommandFromResource(resource, _projectContextFacade);
                var productId = await _productCommandService.Handle(createProductCommand);
                if (productId == Guid.Empty)
                    return BadRequest("ProductId is empty");
                var getProductByIdQuery = new GetProductByIdQuery(productId);
                var product = await _productQueryService.Handle(getProductByIdQuery);
                if (product == null)
                    return StatusCode(500, "Product not found after creation");
                var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
                return CreatedAtAction(nameof(GetProductById), new { productId = product.Id }, productResource);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"ArgumentException: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Exception: {ex.Message}");
            }
        }

        [HttpPatch("{productId}")]
        [ProducesResponseType(typeof(ProductResource), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ProductResource>> UpdateProduct(Guid productId, [FromBody] UpdateProductResource resource)
        {
            try
            {
                if (resource == null)
                    return BadRequest();

                // Update status if provided
                if (!string.IsNullOrWhiteSpace(resource.Status))
                {
                    if (!Enum.TryParse<ProductStatus>(resource.Status, true, out var productStatus))
                        return BadRequest();
                    var updateStatusCommand = new UpdateProductStatusCommand(productId, productStatus);
                    await _productCommandService.Handle(updateStatusCommand);
                }

                // Update price if provided
                if (resource.PriceAmount.HasValue)
                {
                    var currency = !string.IsNullOrWhiteSpace(resource.PriceCurrency) ? resource.PriceCurrency : "PEN";
                    var money = new Money(resource.PriceAmount.Value, currency);
                    var updatePriceCommand = new UpdateProductPriceCommand(productId, money);
                    await _productCommandService.Handle(updatePriceCommand);
                }

                var getProductByIdQuery = new GetProductByIdQuery(productId);
                var product = await _productQueryService.Handle(getProductByIdQuery);
                if (product == null)
                    return NotFound();
                var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
                return Ok(productResource);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"ArgumentException: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Return the exception message and stack trace for debugging (remove in production)
                return StatusCode(500, $"Exception: {ex.Message}\n{ex.StackTrace}");
            }
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            try
            {
                var deleteProductCommand = new DeleteProductCommand(productId);
                await _productCommandService.Handle(deleteProductCommand);
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
    }
}
