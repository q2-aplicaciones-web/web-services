using System;
using System.Collections.Generic;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Queries;
using Q2.Web_Service.API.ProductCatalog.Domain.Services;
using Q2.Web_Service.API.ProductCatalog.Infrastructure.Persistence.EFC.Repositories;

namespace Q2.Web_Service.API.ProductCatalog.Application.Internal.QueryServices
{
    public class ProductQueryServiceImpl : IProductQueryService
    {
        private readonly IProductRepository _productRepository;

        public ProductQueryServiceImpl(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery query)
        {
            return await _productRepository.FindAllAsync();
        }

        public async Task<Product?> Handle(GetProductByIdQuery query)
        {
            return await _productRepository.FindByIdAsync(query.ProductId);
        }

        public async Task<List<Product>> Handle(GetProductsByProjectIdQuery query)
        {
            return await _productRepository.FindByProjectIdAsync(query.ProjectId.Value);
        }
    }
}
