using System.ComponentModel.DataAnnotations;

namespace Q2.WebService.API.ProductCatalog.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for creating a new comment
    /// </summary>
    public class CreateCommentResource
    {
        [Required]
        public string UserId { get; set; }
        
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Text { get; set; }
    }
}
