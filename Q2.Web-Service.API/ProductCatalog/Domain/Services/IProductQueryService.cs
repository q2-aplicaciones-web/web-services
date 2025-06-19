using System.Collections.Generic;
using System.Threading.Tasks;
using Q2.WebService.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.WebService.API.ProductCatalog.Domain.Model.Queries;

namespace Q2.WebService.API.ProductCatalog.Domain.Services
{
    /// <summary>
    /// Interface for product query service
    /// </summary>
    public interface IProductQueryService
    {
        Task<IEnumerable<Product>> Handle(GetAllProductsQuery query);
        Task<Product> Handle(GetProductByIdQuery query);
        Task<IEnumerable<Product>> Handle(GetProductsByProjectIdQuery query);
        Task<IEnumerable<Product>> Handle(SearchProductsByTagsQuery query);
    }
}
