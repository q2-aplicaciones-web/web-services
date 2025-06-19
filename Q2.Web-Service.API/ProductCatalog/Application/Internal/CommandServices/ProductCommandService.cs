using System;
using System.Threading.Tasks;
using Q2.WebService.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.WebService.API.ProductCatalog.Domain.Model.Commands;
using Q2.WebService.API.ProductCatalog.Domain.Model.Entities;
using Q2.WebService.API.ProductCatalog.Domain.Repositories;
using Q2.WebService.API.ProductCatalog.Domain.Services;
using Q2.Web_Service.API.Shared.Domain.Repositories;

namespace Q2.WebService.API.ProductCatalog.Application.Internal.CommandServices
{
    /// <summary>
    /// Implementation of the product command service
    /// </summary>
    public class ProductCommandService : IProductCommandService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductCommandService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateProductCommand command)
        {
            var product = new Product(command);
            await _productRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();
            return product.Id;
        }

        public async Task Handle(UpdateProductPriceCommand command)
        {
            var product = await _productRepository.FindByIdAsync(command.ProductId);
            if (product == null)
                throw new InvalidOperationException($"Product with ID {command.ProductId} not found");

            product.UpdatePrice(command.NewPrice);
            await _productRepository.UpdateAsync(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<Guid> Handle(AddCommentCommand command)
        {
            var product = await _productRepository.FindByIdAsync(command.ProductId);
            if (product == null)
                throw new InvalidOperationException($"Product with ID {command.ProductId} not found");

            var comment = new Comment(command.UserId, command.Text);
            product.AddComment(comment);
            
            await _productRepository.UpdateAsync(product);
            await _unitOfWork.CompleteAsync();
            
            return comment.Id;
        }
    }
}
