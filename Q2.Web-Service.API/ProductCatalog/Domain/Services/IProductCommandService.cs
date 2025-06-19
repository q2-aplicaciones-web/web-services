using System;
using System.Threading.Tasks;
using Q2.WebService.API.ProductCatalog.Domain.Model.Commands;

namespace Q2.WebService.API.ProductCatalog.Domain.Services
{
    /// <summary>
    /// Interface for product command service
    /// </summary>
    public interface IProductCommandService
    {
        Task<Guid> Handle(CreateProductCommand command);
        Task Handle(UpdateProductPriceCommand command);
        Task<Guid> Handle(AddCommentCommand command);
    }
}
