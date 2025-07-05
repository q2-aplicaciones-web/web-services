namespace Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources
{
    public record ProductLikeCountResource
    {
        public long LikeCount { get; }

        public ProductLikeCountResource(long likeCount)
        {
            if (likeCount < 0)
                throw new ArgumentException("Like count cannot be negative", nameof(likeCount));
            LikeCount = likeCount;
        }
    }
}
