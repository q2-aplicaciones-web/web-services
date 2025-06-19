using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;
using Q2.WebService.API.ProductCatalog.Domain.Model.Commands;
using Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects;
using Q2.WebService.API.ProductCatalog.Interfaces.REST.Resources;

namespace Q2.WebService.API.ProductCatalog.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler to convert CreateProductResource to CreateProductCommand
    /// </summary>
    public static class CreateProductCommandFromResourceAssembler
    {
        public static CreateProductCommand ToCommandFromResource(CreateProductResource resource)
        {
            return new CreateProductCommand(
                resource.ProjectId,
                resource.ManufacturerId,
                new Money(resource.Price, resource.Currency),
                resource.Tags,
                resource.Gallery,
                resource.Status
            );
        }
    }
}
