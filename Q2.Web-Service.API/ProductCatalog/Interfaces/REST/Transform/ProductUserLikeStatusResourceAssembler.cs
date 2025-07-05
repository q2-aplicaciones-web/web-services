using System;
using Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Transform
{
    public static class ProductUserLikeStatusResourceAssembler
    {
        public static ProductUserLikeStatusResource ToResourceFromStatus(bool? isLiked)
        {
            // Ensure non-null status
            bool safeStatus = isLiked ?? false;
            return new ProductUserLikeStatusResource(safeStatus);
        }
    }
}
