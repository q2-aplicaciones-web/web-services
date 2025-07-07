using System;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Aggregates;
using Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Transform
{
    /// <summary>
    /// Transforms Product domain entities to ProductResource DTOs.
    /// Part of the anti-corruption layer for the REST interface.
    /// </summary>
    public static class ProductResourceFromEntityAssembler
    {
        public static ProductResource ToResourceFromEntity(Product entity)
        {
            // Ajusta el acceso a ProjectId según tu modelo real:
            // Ajusta el acceso a ProjectId según tu modelo real:
            // Si ProjectId es un objeto, usa ToString(). Si es string, pásalo directo.
            Guid projectId = entity.ProjectId != null ? Guid.Parse(entity.ProjectId.ToString()) : Guid.Empty;

            return new ProductResource(
                entity.Id,
                projectId,
                entity.Price != null ? entity.Price.Amount : 0,
                entity.Price != null ? entity.Price.Currency : string.Empty,
                entity.Status.ToString(),
                entity.ProjectTitle,
                entity.ProjectPreviewUrl,
                entity.UserId,
                entity.LikeCount,
                // Elimina CreatedAt y UpdatedAt si no existen en el modelo
                default,
                default
            );
        }
    }
}
