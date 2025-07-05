using System;
using Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Transform
{
    public static class ProductLikeStatusResourceAssembler
    {
        public static ProductLikeStatusResource ToResourceFromData(string message, Guid productId, Guid userId)
        {
            return new ProductLikeStatusResource(message, productId, userId);
        }

        public static ProductLikeStatusResource ToLikeSuccessResource(Guid productId, Guid userId)
        {
            return ToResourceFromData("Product liked successfully", productId, userId);
        }

        public static ProductLikeStatusResource ToAlreadyLikedResource(Guid productId, Guid userId)
        {
            return ToResourceFromData("Product already liked by user", productId, userId);
        }
    }
}
