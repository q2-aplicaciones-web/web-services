namespace Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for updating a Product's status.
    /// Uses primitive types at the API boundary as part of the anti-corruption layer pattern.
    /// </summary>
    public record UpdateProductStatusResource
    {
        public string Status { get; }

        public UpdateProductStatusResource(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status cannot be null or empty", nameof(status));
            Status = status;
        }
    }
}
