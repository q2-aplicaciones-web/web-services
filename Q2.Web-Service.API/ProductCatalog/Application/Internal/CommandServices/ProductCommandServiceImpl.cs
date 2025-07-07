using System;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Commands;
using Q2.Web_Service.API.ProductCatalog.Domain.Services;
// using Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories;
using Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories;

namespace Q2.Web_Service.API.ProductCatalog.Application.Internal.CommandServices
{
    public class ProductCommandServiceImpl : IProductCommandService
    {
        private readonly IProductRepository _productRepository;

        public ProductCommandServiceImpl(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(CreateProductCommand command)
        {
            var product = new Product(command);
            await _productRepository.SaveAsync(product);
            return product.Id;
        }

        public async Task Handle(UpdateProductPriceCommand command)
        {
            var product = await _productRepository.FindByIdAsync(command.ProductId);
            if (product == null)
                throw new ArgumentException($"Product not found with ID: {command.ProductId}");
            product.UpdatePrice(command.Price);
            await _productRepository.SaveAsync(product);
        }

        public async Task Handle(UpdateProductStatusCommand command)
        {
            var product = await _productRepository.FindByIdAsync(command.ProductId);
            if (product == null)
                throw new ArgumentException($"Product not found with ID: {command.ProductId}");
            product.UpdateStatus(command.Status);
            await _productRepository.SaveAsync(product);
        }

        public async Task Handle(DeleteProductCommand command)
        {
            if (!await _productRepository.ExistsByIdAsync(command.ProductId))
                throw new ArgumentException($"Product not found with ID: {command.ProductId}");
            await _productRepository.DeleteByIdAsync(command.ProductId);
        }
    }
}
