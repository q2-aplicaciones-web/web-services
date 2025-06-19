using Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;

namespace Q2.WebService.API.ProductCatalog.Interfaces.ACL;

/// <summary>
/// Facade interface for the Product Catalog context
/// </summary>
/// <remarks>
/// This facade provides a clean, unified interface for other bounded contexts
/// to interact with the Product Catalog context without directly depending
/// on its internal domain model.
/// </remarks>
public interface IProductCatalogContextFacade
{
    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="projectId">The ID of the project this product is associated with</param>
    /// <param name="manufacturerId">The ID of the manufacturer</param>
    /// <param name="price">The price amount</param>
    /// <param name="currency">The price currency</param>
    /// <param name="tags">List of product tags</param>
    /// <param name="gallery">List of image URLs for the product gallery</param>
    /// <param name="status">The product status</param>
    /// <returns>The created product ID</returns>
    Task<Guid> CreateProductAsync(string projectId, 
        string manufacturerId,
        decimal price, 
        string currency,
        List<string> tags, 
        List<string> gallery,
        string status);

    /// <summary>
    /// Check if a product exists
    /// </summary>
    /// <param name="productId">The ID of the product</param>
    /// <returns>True if the product exists, false otherwise</returns>
    Task<bool> ProductExistsAsync(Guid productId);

    /// <summary>
    /// Get product details by ID
    /// </summary>
    /// <param name="productId">The ID of the product</param>
    /// <returns>ProductInfo containing product details or null if not found</returns>
    Task<ProductInfo?> GetProductInfoAsync(Guid productId);

    /// <summary>
    /// Get products by project ID
    /// </summary>
    /// <param name="projectId">The project ID</param>
    /// <returns>List of product info for all products associated with the project</returns>
    Task<List<ProductInfo>> GetProductsByProjectAsync(string projectId);

    /// <summary>
    /// Update product price
    /// </summary>
    /// <param name="productId">The ID of the product</param>
    /// <param name="price">The new price amount</param>
    /// <param name="currency">The price currency</param>
    /// <returns>True if update was successful, false otherwise</returns>
    Task<bool> UpdateProductPriceAsync(Guid productId, decimal price, string currency);

    /// <summary>
    /// Add comment to a product
    /// </summary>
    /// <param name="productId">The ID of the product</param>
    /// <param name="userId">The ID of the user adding the comment</param>
    /// <param name="text">The comment text</param>
    /// <returns>The created comment ID or empty Guid if creation failed</returns>
    Task<Guid> AddCommentAsync(Guid productId, string userId, string text);

    /// <summary>
    /// Search products by tags
    /// </summary>
    /// <param name="tags">List of tags to search for</param>
    /// <returns>List of matching product IDs</returns>
    Task<List<Guid>> SearchProductsByTagsAsync(List<string> tags);
}

/// <summary>
/// Value object for transferring product information across bounded contexts
/// </summary>
public record ProductInfo(
    Guid Id,
    ProjectId ProjectId,
    ManufacturerId ManufacturerId,
    Money Price,  // Price already contains Currency property
    int Likes,
    List<string> Tags,
    List<string> Gallery,
    double Rating,
    string Status
);
