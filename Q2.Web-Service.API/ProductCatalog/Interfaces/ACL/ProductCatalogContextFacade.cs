using System;
using System.Collections.Generic;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.ProductCatalog.Interfaces.ACL.ProductCatalogContextFacadeNS
{
    public interface IProductCatalogContextFacade
    {
        Task<Guid> CreateProduct(string projectId, decimal price, string currency, string status);
        bool ProductExists(Guid productId);
        ProductInfo GetProductInfo(Guid productId);
        List<ProductInfo> GetProductsByProject(string projectId);
        bool UpdateProductPrice(Guid productId, decimal price, string currency);
        bool ProjectHasProducts(string projectId);
    }

    public record ProductInfo(
        Guid Id,
        Guid ProjectId,
        Money Price,
        ProductStatus Status,
        string ProjectTitle,
        string ProjectPreviewUrl,
        Guid ProjectUserId,
        long LikeCount
    );
}
