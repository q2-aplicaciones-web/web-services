using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories;

// Ensure the interface is referenced from the correct file and namespace
namespace Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories
{
public class ProductRepository : IProductRepository
{
    private readonly Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration.AppDbContext _dbContext;

    public ProductRepository(Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration.AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> FindByIdAsync(Guid productId)
    {
        return await _dbContext.Products.FindAsync(productId);
    }

    public async Task SaveAsync(Product product)
    {
        if (product.Id == Guid.Empty)
            product.Id = Guid.NewGuid();

        var exists = await _dbContext.Products.AnyAsync(p => p.Id == product.Id);
        if (!exists)
        {
            await _dbContext.Products.AddAsync(product);
        }
        else
        {
            _dbContext.Products.Update(product);
            // Ensure EF Core tracks changes to the owned Price value object
            _dbContext.Entry(product).Reference(p => p.Price).IsModified = true;
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsByIdAsync(Guid productId)
    {
        return await _dbContext.Products.AnyAsync(p => p.Id == productId);
    }

    public async Task DeleteByIdAsync(Guid productId)
    {
        var product = await _dbContext.Products.FindAsync(productId);
        if (product != null)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<Product>> FindAllAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<List<Product>> FindByProjectIdAsync(string projectId)
    {
        var all = await _dbContext.Products.ToListAsync();
        return all.Where(p => p.ProjectId != null && p.ProjectId.Value == projectId).ToList();
    }
}
}
