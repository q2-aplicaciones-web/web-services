using System;
using Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources;

namespace Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for creating a new Product.
    /// Uses primitive types at the API boundary as part of the anti-corruption layer pattern.
    /// </summary>
public class CreateProductResource
{
    public string? ProjectId { get; set; }
    public decimal PriceAmount { get; set; }
    public string? PriceCurrency { get; set; }
    public string? Status { get; set; }

    public CreateProductResource() { }

    public CreateProductResource(string projectId, decimal priceAmount, string priceCurrency, string? status)
    {
        ProjectId = projectId;
        PriceAmount = priceAmount;
        PriceCurrency = priceCurrency;
        Status = status;
    }
}
}
