using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates;

namespace Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> FindByIdAsync(Guid productId);
        Task SaveAsync(Product product);
        Task<bool> ExistsByIdAsync(Guid productId);
        Task DeleteByIdAsync(Guid productId);
        Task<List<Product>> FindAllAsync();
        Task<List<Product>> FindByProjectIdAsync(string projectId);
    }
}
