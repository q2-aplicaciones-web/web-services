using Q2.WebService.API.ProductCatalog.Domain.Model.Entities;
using Q2.WebService.API.ProductCatalog.Interfaces.REST.Resources;

namespace Q2.WebService.API.ProductCatalog.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler to convert Comment entities to CommentResource
    /// </summary>
    public static class CommentResourceFromEntityAssembler
    {
        public static CommentResource ToResourceFromEntity(Comment entity)
        {
            if (entity == null) return null;

            return new CommentResource
            {
                Id = entity.Id.ToString(),
                UserId = entity.UserId.ToString(),
                Text = entity.Text,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
