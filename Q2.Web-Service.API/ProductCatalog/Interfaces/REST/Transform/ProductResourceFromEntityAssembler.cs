using System.Collections.Generic;
using System.Linq;
using Q2.WebService.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.WebService.API.ProductCatalog.Interfaces.REST.Resources;

namespace Q2.WebService.API.ProductCatalog.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler to convert Product entities to ProductResource
    /// </summary>
    public static class ProductResourceFromEntityAssembler
    {
        public static ProductResource ToResourceFromEntity(Product entity)
        {
            if (entity == null) return null;

            return new ProductResource
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId.Value,
                ManufacturerId = entity.ManufacturerId.Value,
                Price = entity.Price.Amount,
                Currency = entity.Price.Currency,
                Likes = entity.Likes,
                Tags = entity.Tags.ToList(),
                Gallery = entity.Gallery.ToList(),
                Rating = entity.Rating.Value,
                Status = entity.Status,
                Comments = entity.Comments.Select(CommentResourceFromEntityAssembler.ToResourceFromEntity).ToList(),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static IEnumerable<ProductResource> ToResourceFromEntityList(IEnumerable<Product> entities)
        {
            return entities?.Select(ToResourceFromEntity).ToList();
        }
    }
}
