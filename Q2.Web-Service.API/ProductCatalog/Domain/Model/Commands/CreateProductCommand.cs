using System.Collections.Generic;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;
using Q2.WebService.API.ProductCatalog.Domain.Model.ValueObjects;

namespace Q2.WebService.API.ProductCatalog.Domain.Model.Commands
{
    /// <summary>
    /// Command to create a new product
    /// </summary>
    public record CreateProductCommand(
        ProjectId ProjectId,
        ManufacturerId ManufacturerId,
        Money Price,
        List<string> Tags,
        List<string> Gallery,
        string Status)
    {
        /// <summary>
        /// Alternative constructor accepting string IDs
        /// </summary>
        public CreateProductCommand(
            string projectId,
            string manufacturerId,
            Money price,
            List<string> tags,
            List<string> gallery,
            string status)
            : this(
                ProjectId.Of(projectId),
                ManufacturerId.Of(manufacturerId),
                price,
                tags,
                gallery,
                status)
        {
        }
    }
}
