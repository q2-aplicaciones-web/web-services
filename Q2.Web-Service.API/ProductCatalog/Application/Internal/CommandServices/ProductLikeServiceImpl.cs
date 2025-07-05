using System;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Commands;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Queries;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects;
using Q2.Web_Service.API.ProductCatalog.Domain.Services;
using Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.repositories;
using Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories;

namespace Q2.Web_Service.API.ProductCatalog.Application.Internal.CommandServices
{
    public class ProductLikeServiceImpl : IProductLikeService
    {
        private readonly IProductLikeRepository _productLikeRepository;
        private readonly IProductRepository _productRepository;

        public ProductLikeServiceImpl(IProductLikeRepository productLikeRepository, IProductRepository productRepository)
        {
            _productLikeRepository = productLikeRepository;
            _productRepository = productRepository;
        }

        public async Task Handle(LikeProductCommand command)
        {
            var userId = UserId.Of(command.UserId);
            var product = await _productRepository.FindByIdAsync(command.ProductId);
            if (product == null)
                throw new ArgumentException($"Product not found with ID: {command.ProductId}");
            if (await _productLikeRepository.ExistsByProductIdAndUserIdAsync(command.ProductId, userId))
                return;
            var productLike = new ProductLike(command.ProductId, command.UserId);
            await _productLikeRepository.SaveAsync(productLike);
            product.IncrementLikeCount();
            await _productRepository.SaveAsync(product);
            // Optionally: publish domain event here
        }

        public async Task Handle(UnlikeProductCommand command)
        {
            var userId = UserId.Of(command.UserId);
            var product = await _productRepository.FindByIdAsync(command.ProductId);
            if (product == null)
                throw new ArgumentException($"Product not found with ID: {command.ProductId}");
            var existingLike = await _productLikeRepository.FindByProductIdAndUserIdAsync(command.ProductId, userId);
            if (existingLike == null)
                return;
            await _productLikeRepository.DeleteAsync(existingLike);
            product.DecrementLikeCount();
            await _productRepository.SaveAsync(product);
            // Optionally: publish domain event here
        }

        public async Task<bool> Handle(CheckIfUserLikedProductQuery query)
        {
            var userId = UserId.Of(query.UserId);
            return await _productLikeRepository.ExistsByProductIdAndUserIdAsync(query.ProductId, userId);
        }

        public async Task<long> Handle(GetLikeCountByProductQuery query)
        {
            var product = await _productRepository.FindByIdAsync(query.ProductId);
            return product?.LikeCount ?? 0L;
        }
    }
}
