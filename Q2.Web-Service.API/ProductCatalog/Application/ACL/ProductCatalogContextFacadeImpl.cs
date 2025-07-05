
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Commands;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Queries;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects;
using Q2.Web_Service.API.ProductCatalog.Domain.Services;
// using Q2.Web_Service.API.ProductCatalog.Interfaces.ACL;
using Q2.Web_Service.API.ProductCatalog.Interfaces.ACL.ProductCatalogContextFacadeNS;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;
using Q2.Web_Service.API.ProductCatalog.Interfaces.ACL;

namespace Q2.Web_Service.API.ProductCatalog.Application.ACL
{
    public class ProductCatalogContextFacadeImpl : IProductCatalogContextFacade
    {
        private const string DEFAULT_CURRENCY = "PEN";

        private readonly IProductCommandService _productCommandService;
        private readonly IProductQueryService _productQueryService;
        private readonly IProjectContextFacade _projectContextFacade;

        public ProductCatalogContextFacadeImpl(
            IProductCommandService productCommandService,
            IProductQueryService productQueryService,
            IProjectContextFacade projectContextFacade)
        {
            _productCommandService = productCommandService;
            _productQueryService = productQueryService;
            _projectContextFacade = projectContextFacade;
        }

        public async Task<Guid> CreateProduct(string projectId, decimal price, string currency, string status)
        {
            if (string.IsNullOrWhiteSpace(projectId))
                throw new ArgumentException("Project ID cannot be null or empty", nameof(projectId));
            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero", nameof(price));

            if (!Guid.TryParse(projectId, out var projectUuid))
                throw new ArgumentException($"Invalid project ID format: {projectId}");

            if (!_projectContextFacade.ProjectExists(projectUuid))
                throw new ArgumentException($"Project does not exist with ID: {projectId}");

            var projectDetails = _projectContextFacade.FetchProjectDetailsForProduct(projectUuid);
            if (projectDetails == null)
                throw new ArgumentException($"Project details not found for ID: {projectId}");

            ProductStatus productStatus = ProductStatus.AVAILABLE;
            if (!string.IsNullOrWhiteSpace(status))
            {
                if (!Enum.TryParse<ProductStatus>(status, true, out productStatus))
                    throw new ArgumentException($"Invalid status: {status}. Valid statuses are: AVAILABLE, UNAVAILABLE, OUT_OF_STOCK, DISCONTINUED");
            }

            string currencyToUse = !string.IsNullOrWhiteSpace(currency) ? currency : DEFAULT_CURRENCY;

            var createProductCommand = new CreateProductCommand(
                ProjectId.Of(projectUuid),
                new Money(price, currencyToUse),
                productStatus,
                projectDetails.Title,
                projectDetails.PreviewUrl,
                projectDetails.UserId
            );

            return await _productCommandService.Handle(createProductCommand);
        }

        public bool ProductExists(Guid productId)
        {
            var query = new GetProductByIdQuery(productId);
            var productOptionalTask = _productQueryService.Handle(query);
            var productOptional = productOptionalTask.GetAwaiter().GetResult();
            return productOptional != null;
        }

        public ProductInfo GetProductInfo(Guid productId)
        {
            var query = new GetProductByIdQuery(productId);
            var productOptionalTask = _productQueryService.Handle(query);
            var productOptional = productOptionalTask.GetAwaiter().GetResult();
            return productOptional != null ? MapToProductInfo(productOptional) : null!;
        }

        public List<ProductInfo> GetProductsByProject(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
                throw new ArgumentException("Project ID cannot be null or empty", nameof(projectId));
            if (!Guid.TryParse(projectId, out _))
                throw new ArgumentException($"Invalid project ID format: {projectId}");

            var query = new GetProductsByProjectIdQuery(projectId);
            var productsTask = _productQueryService.Handle(query);
            var products = productsTask.GetAwaiter().GetResult();
            return products.Select(MapToProductInfo).ToList();
        }

        public bool UpdateProductPrice(Guid productId, decimal price, string currency)
        {
            try
            {
                string currencyToUse = !string.IsNullOrWhiteSpace(currency) ? currency : DEFAULT_CURRENCY;
                var command = new UpdateProductPriceCommand(productId, new Money(price, currencyToUse));
                _productCommandService.Handle(command).GetAwaiter().GetResult();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ProjectHasProducts(string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
                return false;
            if (!Guid.TryParse(projectId, out _))
                return false;

            var query = new GetProductsByProjectIdQuery(projectId);
            var productsTask = _productQueryService.Handle(query);
            var products = productsTask.GetAwaiter().GetResult();
            return products != null && products.Any();
        }

        private ProductInfo MapToProductInfo(Product product)
        {
            // Usa ToString() para obtener el valor del ProjectId si no tiene 'Value'
            return new ProductInfo(
                product.Id,
                product.ProjectId != null ? Guid.Parse(product.ProjectId.ToString()) : Guid.Empty,
                product.Price,
                product.Status,
                product.ProjectTitle,
                product.ProjectPreviewUrl,
                product.ProjectUserId,
                product.LikeCount
            );
        }
    }
}

