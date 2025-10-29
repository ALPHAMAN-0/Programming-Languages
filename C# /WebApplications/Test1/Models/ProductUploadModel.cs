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

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile? Image { get; set; }

        public string[] Sizes { get; set; } = Array.Empty<string>();
    }
}