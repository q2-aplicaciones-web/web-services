using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Q2.WebService.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects;
using Q2.WebService.API.ProductCatalog.Domain.Repositories;

namespace Q2.WebService.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Implementation of the product repository
/// </summary>
public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
{
    public async Task<Product?> FindByIdAsync(Guid id)
    {
        return await Context.Set<Product>()
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> FindAllAsync()
    {
        return await Context.Set<Product>()
            .Include(p => p.Comments)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> FindByProjectIdAsync(ProjectId projectId)
    {
        return await Context.Set<Product>()
            .Include(p => p.Comments)
            .Where(p => p.ProjectId.Value == projectId.Value)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> FindByTagsAsync(List<string> tags)
    {
        if (tags == null || !tags.Any())
            return new List<Product>();

        return await Context.Set<Product>()
            .Include(p => p.Comments)
            .Where(p => p.Tags.Any(t => tags.Contains(t)))
            .ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await Context.Set<Product>().AddAsync(product);
    }

    public Task UpdateAsync(Product product)
    {
        Context.Set<Product>().Update(product);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(Product product)
    {
        Context.Set<Product>().Remove(product);
        return Task.CompletedTask;
    }
}