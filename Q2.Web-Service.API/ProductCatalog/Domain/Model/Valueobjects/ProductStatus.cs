namespace Q2.Web_Service.API.ProductCatalog.Domain.Model.ValueObjects
{
    /// <summary>
    /// Value object representing the status of a product in the catalog.
    /// Defines the possible states a product can have in the system.
    /// </summary>
    public enum ProductStatus
    {
        /// <summary>
        /// Product is available for purchase and display in the catalog.
        /// </summary>
        AVAILABLE,

        /// <summary>
        /// Product is temporarily unavailable but may become available again.
        /// </summary>
        UNAVAILABLE,

        /// <summary>
        /// Product is out of stock and cannot be purchased.
        /// </summary>
        OUT_OF_STOCK,

        /// <summary>
        /// Product has been discontinued and will not be restocked.
        /// </summary>
        DISCONTINUED
    }
}
