using Microsoft.AspNetCore.Mvc;
using Test1.Models;
using System.IO;

namespace Test1.Controllers
{
    public class NewbornController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewbornController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var newbornProductsPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "newborn");
            var products = new List<Product>();

            if (Directory.Exists(newbornProductsPath))
            {
                var imageFiles = Directory.GetFiles(newbornProductsPath)
                    .Where(file => file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg"));

                foreach (var imageFile in imageFiles)
                {
                    products.Add(new Product
                    {
                        Name = Path.GetFileNameWithoutExtension(imageFile),
                        ImagePath = $"/images/newborn/{Path.GetFileName(imageFile)}",
                        Price = 49.99M,
                        Category = "Newborn",
                        IsAvailable = true
                    });
                }
            }

            return View(products);
        }
    }
}