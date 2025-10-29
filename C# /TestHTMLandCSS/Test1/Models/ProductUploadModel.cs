using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Test1.Models
{
    public class ProductUploadModel
    {
        public ProductUploadModel()
        {
            Image = null!;
        }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please select an image")]
        public IFormFile? Image { get; set; }

        public string[] Sizes { get; set; } = Array.Empty<string>();
    }
}