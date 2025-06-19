using System;
using Q2.WebService.API.ProductCatalog.Domain.Model.Commands;
using Q2.WebService.API.ProductCatalog.Interfaces.REST.Resources;

namespace Q2.WebService.API.ProductCatalog.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler to convert CreateCommentResource to AddCommentCommand
    /// </summary>
    public static class AddCommentCommandFromResourceAssembler
    {
        public static AddCommentCommand ToCommandFromResource(Guid productId, CreateCommentResource resource)
        {
            return new AddCommentCommand(
                productId.ToString(),
                resource.UserId,
                resource.Text
            );
        }
    }
}
