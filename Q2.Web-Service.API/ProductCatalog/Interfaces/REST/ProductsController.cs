using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Q2.WebService.API.ProductCatalog.Domain.Model.Commands;
using Q2.WebService.API.ProductCatalog.Domain.Model.Queries;
using Q2.WebService.API.ProductCatalog.Domain.Services;
using Q2.WebService.API.ProductCatalog.Interfaces.REST.Resources;
using Q2.WebService.API.ProductCatalog.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Q2.WebService.API.ProductCatalog.Interfaces.REST
{
    /// <summary>    /// Products controller for the API
    /// </summary>
    [ApiController]
    [Route("api/v1/products")]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerTag("Available Product Endpoints")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductCommandService _productCommandService;
        private readonly IProductQueryService _productQueryService;

        public ProductsController(IProductCommandService productCommandService, IProductQueryService productQueryService)
        {
            _productCommandService = productCommandService;
            _productQueryService = productQueryService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all products",
            Description = "Get all products from the catalog",
            OperationId = "GetAllProducts")]
        [SwaggerResponse(200, "Products found", typeof(IEnumerable<ProductResource>))]
        [SwaggerResponse(404, "No products found")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productQueryService.Handle(new GetAllProductsQuery());
            if (!products.Any())
                return NotFound();

            var productResources = products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(productResources);
        }

        [HttpGet("{productId:guid}")]
        [SwaggerOperation(
            Summary = "Get product by ID",
            Description = "Get a specific product by its ID",
            OperationId = "GetProductById")]
        [SwaggerResponse(200, "Product found", typeof(ProductResource))]
        [SwaggerResponse(404, "Product not found")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var product = await _productQueryService.Handle(new GetProductByIdQuery(productId));
            if (product == null)
                return NotFound();

            var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
            return Ok(productResource);
        }

        [HttpGet("by-project/{projectId}")]
        [SwaggerOperation(
            Summary = "Get products by project ID",
            Description = "Get all products associated with a specific project",
            OperationId = "GetProductsByProjectId")]
        [SwaggerResponse(200, "Products found", typeof(IEnumerable<ProductResource>))]
        [SwaggerResponse(404, "No products found for this project")]
        public async Task<IActionResult> GetProductsByProjectId(string projectId)
        {
            var products = await _productQueryService.Handle(new GetProductsByProjectIdQuery(projectId));
            if (!products.Any())
                return NotFound();

            var productResources = products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(productResources);
        }

        [HttpGet("search")]
        [SwaggerOperation(
            Summary = "Search products by tags",
            Description = "Search products that match any of the provided tags",
            OperationId = "SearchProductsByTags")]
        [SwaggerResponse(200, "Products found", typeof(IEnumerable<ProductResource>))]
        [SwaggerResponse(404, "No products found with these tags")]
        public async Task<IActionResult> SearchProductsByTags([FromQuery] List<string> tags)
        {
            var products = await _productQueryService.Handle(new SearchProductsByTagsQuery(tags));
            if (!products.Any())
                return NotFound();

            var productResources = products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(productResources);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create product",
            Description = "Create a new product in the catalog",
            OperationId = "CreateProduct")]
        [SwaggerResponse(201, "Product created successfully", typeof(ProductResource))]
        [SwaggerResponse(400, "Invalid product data")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductResource resource)
        {
            var command = CreateProductCommandFromResourceAssembler.ToCommandFromResource(resource);
            var productId = await _productCommandService.Handle(command);

            var product = await _productQueryService.Handle(new GetProductByIdQuery(productId));
            if (product == null)
                return BadRequest();            var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
            return CreatedAtAction(nameof(GetProductById), new { productId }, productResource);
        }
    }
}
