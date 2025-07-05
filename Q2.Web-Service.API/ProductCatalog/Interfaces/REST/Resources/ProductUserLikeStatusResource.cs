namespace Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources
{
    public record ProductUserLikeStatusResource
    {
        public bool IsLiked { get; }

        public ProductUserLikeStatusResource(bool isLiked)
        {
            IsLiked = isLiked;
        }
    }
}
