using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Q2.WebService.API.ProductCatalog.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for creating a new product
    /// </summary>
    public class CreateProductResource
    {
        [Required]
        public string ProjectId { get; set; }
        
        [Required]
        public string ManufacturerId { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Currency { get; set; } = "USD";
        
        public List<string> Tags { get; set; } = new();
        
        public List<string> Gallery { get; set; } = new();
        
        [Required]
        public string Status { get; set; } = "Available";
    }
}
