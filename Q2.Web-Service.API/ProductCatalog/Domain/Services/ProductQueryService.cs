using System;
using System.Collections.Generic;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Queries;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Services
{
    public interface IProductQueryService
    {
        Task<List<Product>> Handle(GetAllProductsQuery query);
        Task<Product?> Handle(GetProductByIdQuery query);
        Task<List<Product>> Handle(GetProductsByProjectIdQuery query);
    }
}
