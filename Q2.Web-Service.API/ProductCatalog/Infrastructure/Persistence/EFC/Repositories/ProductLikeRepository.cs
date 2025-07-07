using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.repositories
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

    public class ProductLikeRepository : IProductLikeRepository
    {
        private readonly Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration.AppDbContext _context;

        public ProductLikeRepository(Q2.Web_Service.API.Shared.Infrastructure.Persistence.EFC.Configuration.AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductLike?> FindByProductIdAndUserIdAsync(Guid productId, UserId userId)
        {
            return await _context.Set<ProductLike>()
                .FirstOrDefaultAsync(pl => pl.ProductId == productId && pl.UserId == userId.Id);
        }

        public async Task<bool> ExistsByProductIdAndUserIdAsync(Guid productId, UserId userId)
        {
            return await _context.Set<ProductLike>()
                .AnyAsync(pl => pl.ProductId == productId && pl.UserId == userId.Id);
        }

        public async Task<long> CountByProductIdAsync(Guid productId)
        {
            return await _context.Set<ProductLike>()
                .LongCountAsync(pl => pl.ProductId == productId);
        }

        public async Task DeleteByProductIdAndUserIdAsync(Guid productId, UserId userId)
        {
            var like = await FindByProductIdAndUserIdAsync(productId, userId);
            if (like != null)
            {
                _context.Set<ProductLike>().Remove(like);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync(ProductLike productLike)
        {
            _context.Set<ProductLike>().Add(productLike);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductLike productLike)
        {
            _context.Set<ProductLike>().Remove(productLike);
            await _context.SaveChangesAsync();
        }
    }
}
