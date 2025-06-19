using System.Collections.Generic;
using System.Threading.Tasks;
using Q2.WebService.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.WebService.API.ProductCatalog.Domain.Model.Queries;
using Q2.WebService.API.ProductCatalog.Domain.Repositories;
using Q2.WebService.API.ProductCatalog.Domain.Services;

namespace Q2.WebService.API.ProductCatalog.Application.Internal.QueryServices
{
    /// <summary>
    /// Implementation of the product query service
    /// </summary>
    public class ProductQueryService : IProductQueryService
    {
        private readonly IProductRepository _productRepository;

        public ProductQueryService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery query)
        {
            return await _productRepository.FindAllAsync();
        }

        public async Task<Product> Handle(GetProductByIdQuery query)
        {
            return await _productRepository.FindByIdAsync(query.ProductId);
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsByProjectIdQuery query)
        {
            return await _productRepository.FindByProjectIdAsync(query.ProjectId);
        }

        public async Task<IEnumerable<Product>> Handle(SearchProductsByTagsQuery query)
        {
            return await _productRepository.FindByTagsAsync(query.Tags);
        }
    }
}
