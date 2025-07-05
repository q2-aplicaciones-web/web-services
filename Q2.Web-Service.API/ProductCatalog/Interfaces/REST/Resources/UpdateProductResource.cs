namespace Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for updating a Product.
    /// Uses primitive types at the API boundary as part of the anti-corruption layer pattern.
    /// Only includes fields that can be updated.
    /// </summary>
    public record UpdateProductResource
    {
        public decimal? PriceAmount { get; init; }
        public string? PriceCurrency { get; init; }
        public string? Status { get; init; }

        public UpdateProductResource(decimal? priceAmount, string? priceCurrency, string? status)
        {
            PriceAmount = priceAmount;
            PriceCurrency = priceCurrency;
            Status = status;
        }
    }
}
