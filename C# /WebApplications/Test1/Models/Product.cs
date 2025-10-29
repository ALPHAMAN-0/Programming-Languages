using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test1.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Image path is required")]
        public string ImagePath { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;

        [NotMapped]
        public string[] Sizes { get; set; } = Array.Empty<string>();
    }
}