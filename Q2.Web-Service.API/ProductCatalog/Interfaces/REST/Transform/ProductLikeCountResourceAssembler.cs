using System;
using Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Transform
{
    public static class ProductLikeCountResourceAssembler
    {
        public static ProductLikeCountResource ToResourceFromCount(long? likeCount)
        {
            // Ensure non-null and non-negative count
            long safeCount = (likeCount.HasValue && likeCount.Value >= 0) ? likeCount.Value : 0L;
            return new ProductLikeCountResource(safeCount);
        }
    }
}
