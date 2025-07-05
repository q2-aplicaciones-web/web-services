using System;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Commands;

namespace Q2.Web_Service.API.ProductCatalog.Domain.Services
{
    public interface IProductCommandService
    {
        Task<Guid> Handle(CreateProductCommand command);
        Task Handle(UpdateProductPriceCommand command);
        Task Handle(UpdateProductStatusCommand command);
        Task Handle(DeleteProductCommand command);
    }
}
