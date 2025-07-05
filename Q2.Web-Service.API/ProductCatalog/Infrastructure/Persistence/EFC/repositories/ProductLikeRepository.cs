using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories
{
    public interface IProductLikeRepository
    {
        Task<ProductLike?> FindByProductIdAndUserIdAsync(Guid productId, UserId userId);
        Task<bool> ExistsByProductIdAndUserIdAsync(Guid productId, UserId userId);
        Task<long> CountByProductIdAsync(Guid productId);
        Task DeleteByProductIdAndUserIdAsync(Guid productId, UserId userId);
        Task SaveAsync(ProductLike productLike);
        Task DeleteAsync(ProductLike productLike);
    }
}
