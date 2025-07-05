using System;
using System.Globalization;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.Commands;
using Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects;
using Q2.Web_Service.API.Shared.Domain.Model.ValueObjects;
using Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Resources;
using Q2.Web_Service.API.ProductCatalog.Interfaces.ACL;

namespace Q2.Web_Service.API.ProductCatalog.Interfaces.REST.Transform
{
    /// <summary>
    /// Transforms CreateProductResource DTOs into CreateProductCommand domain objects.
    /// Part of the anti-corruption layer for the REST interface.
    /// </summary>
    public static class CreateProductCommandFromResourceAssembler
    {
        public static CreateProductCommand ToCommandFromResource(
            CreateProductResource resource,
            Q2.Web_Service.API.ProductCatalog.Interfaces.ACL.IProjectContextFacade projectContextFacade)
        {

            // Validate ProjectId
            if (string.IsNullOrWhiteSpace(resource.ProjectId))
                throw new ArgumentException("ProjectId is required");

            // Get project details from DesignLab
            var projectDetails = projectContextFacade.FetchProjectDetailsForProduct(
                Guid.Parse(resource.ProjectId)
            );

            if (projectDetails == null)
                throw new ArgumentException($"Project not found with ID: {resource.ProjectId}");

            // Parse currency
            var currencyCode = !string.IsNullOrWhiteSpace(resource.PriceCurrency)
                ? resource.PriceCurrency
                : Money.DEFAULT_CURRENCY;
            var currency = new RegionInfo(currencyCode);

            // Parse status or default to AVAILABLE
            ProductStatus status = ProductStatus.AVAILABLE;
            if (!string.IsNullOrWhiteSpace(resource.Status))
            {
                if (!Enum.TryParse<ProductStatus>(resource.Status, true, out status))
                {
                    throw new ArgumentException($"Invalid status: {resource.Status}. Valid statuses are: AVAILABLE, UNAVAILABLE, OUT_OF_STOCK, DISCONTINUED");
                }
            }

            return new CreateProductCommand(
                ProjectId.Of(resource.ProjectId),
                new Money(resource.PriceAmount, currency.ISOCurrencySymbol),
                status,
                projectDetails.Title,
                projectDetails.PreviewUrl,
                resource.UserId
            );
        }
    }
}
