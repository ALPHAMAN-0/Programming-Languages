using Microsoft.AspNetCore.Mvc;
using Test1.Models;
using System.IO;

namespace Test1.Controllers
{
    public class WomenController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WomenController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var womenProductsPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "women");
            var products = new List<Product>();

            if (Directory.Exists(womenProductsPath))
            {
                var imageFiles = Directory.GetFiles(womenProductsPath)
                    .Where(file => file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg"));

                foreach (var imageFile in imageFiles)
                {
                    products.Add(new Product
                    {
                        Name = Path.GetFileNameWithoutExtension(imageFile),
                        ImagePath = $"/images/women/{Path.GetFileName(imageFile)}",
                        Price = 89.99M,
                        Category = "Women",
                        IsAvailable = true
                    });
                }
            }

            return View(products);
        }
    }
}