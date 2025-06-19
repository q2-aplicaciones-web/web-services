using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Q2.WebService.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects;
using Q2.WebService.API.ProductCatalog.Domain.Repositories;
using Q2.WebService.API.ProductCatalog.Infrastructure.Persistence.EFC.Configuration;

namespace Q2.WebService.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories
{
    /// <summary>
    /// Implementation of the product repository
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ProductCatalogDbContext _dbContext;

        public ProductRepository(ProductCatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> FindByIdAsync(Guid id)
        {
            return await _dbContext.Products
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> FindAllAsync()
        {
            return await _dbContext.Products
                .Include(p => p.Comments)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> FindByProjectIdAsync(ProjectId projectId)
        {
            return await _dbContext.Products
                .Include(p => p.Comments)
                .Where(p => p.ProjectId.Value == projectId.Value)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> FindByTagsAsync(List<string> tags)
        {
            if (tags == null || !tags.Any())
                return new List<Product>();

            // Using EF Core's Any and string.Contains to find products with any of the tags
            return await _dbContext.Products
                .Include(p => p.Comments)
                .Where(p => p.Tags.Any(t => tags.Contains(t)))
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public Task UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            return Task.CompletedTask;
        }
    }
}
