using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Q2.WebService.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects;

namespace Q2.WebService.API.ProductCatalog.Domain.Repositories
{
    /// <summary>
    /// Interface for product repository
    /// </summary>
    public interface IProductRepository
    {
        Task<Product> FindByIdAsync(Guid id);
        Task<IEnumerable<Product>> FindAllAsync();
        Task<IEnumerable<Product>> FindByProjectIdAsync(ProjectId projectId);
        Task<IEnumerable<Product>> FindByTagsAsync(List<string> tags);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(Product product);
    }
}
