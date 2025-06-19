using Q2.WebService.API.ProductCatalog.Domain.Model.Commands;
using Q2.WebService.API.ProductCatalog.Domain.Model.Queries;
using Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects;
using Q2.WebService.API.ProductCatalog.Domain.Services;
using Q2.WebService.API.ProductCatalog.Interfaces.ACL;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;

namespace Q2.WebService.API.ProductCatalog.Application.ACL;

/// <summary>
/// Implementation of the Product Catalog Context Facade
/// </summary>
/// <remarks>
/// This facade provides a clean, unified interface for other bounded contexts
/// to interact with the Product Catalog context without directly depending
/// on its internal domain model.
/// </remarks>
public class ProductCatalogContextFacade : IProductCatalogContextFacade
{
    private const string DEFAULT_CURRENCY = "USD";
    
    private readonly IProductCommandService _productCommandService;
    private readonly IProductQueryService _productQueryService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="productCommandService">The product command service</param>
    /// <param name="productQueryService">The product query service</param>
    public ProductCatalogContextFacade(
        IProductCommandService productCommandService,
        IProductQueryService productQueryService)
    {
        _productCommandService = productCommandService;
        _productQueryService = productQueryService;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateProductAsync(string projectId, string manufacturerId, decimal price, string currency,
        List<string> tags, List<string> gallery, string status)
    {
        var createProductCommand = new CreateProductCommand(
            projectId,
            manufacturerId,
            new Money(price, currency),
            tags,
            gallery,
            status);

        return await _productCommandService.Handle(createProductCommand);
    }

    /// <inheritdoc />
    public async Task<bool> ProductExistsAsync(Guid productId)
    {
        var query = new GetProductByIdQuery(productId);
        var product = await _productQueryService.Handle(query);
        return product != null;
    }

    /// <inheritdoc />
    public async Task<ProductInfo?> GetProductInfoAsync(Guid productId)
    {
        var query = new GetProductByIdQuery(productId);
        var product = await _productQueryService.Handle(query);
        
        if (product == null)
            return null;        return new ProductInfo(
            product.Id,
            product.ProjectId,
            product.ManufacturerId,
            product.Price,
            product.Likes,
            product.Tags.ToList(),
            product.Gallery.ToList(),
            product.Rating.Value,
            product.Status
        );
    }

    /// <inheritdoc />
    public async Task<List<ProductInfo>> GetProductsByProjectAsync(string projectId)
    {
        var query = new GetProductsByProjectIdQuery(projectId);
        var products = await _productQueryService.Handle(query);
          return products.Select(product => new ProductInfo(
            product.Id,
            product.ProjectId,
            product.ManufacturerId,
            product.Price,
            product.Likes,
            product.Tags.ToList(),
            product.Gallery.ToList(),
            product.Rating.Value,
            product.Status
        )).ToList();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateProductPriceAsync(Guid productId, decimal price, string currency)
    {
        try
        {
            var command = new UpdateProductPriceCommand(
                productId,
                new Money(price, currency));
                
            await _productCommandService.Handle(command);
            return true;
        }
        catch
        {
            return false;
        }
    }    /// <inheritdoc />
    public async Task<Guid> AddCommentAsync(Guid productId, string userId, string text)
    {
        try
        {
            var command = new AddCommentCommand(
                productId,
                UserId.Of(userId),
                text);
                
            return await _productCommandService.Handle(command);
        }
        catch
        {
            return Guid.Empty;
        }
    }

    /// <inheritdoc />
    public async Task<List<Guid>> SearchProductsByTagsAsync(List<string> tags)
    {
        var query = new SearchProductsByTagsQuery(tags);
        var products = await _productQueryService.Handle(query);
        
        return products.Select(product => product.Id).ToList();
    }
}
