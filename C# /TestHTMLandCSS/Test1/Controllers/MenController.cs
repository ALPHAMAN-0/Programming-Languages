using Microsoft.AspNetCore.Mvc;
using Test1.Models;  // Use Product from Models namespace
using Test1.Services;
using System.IO;

namespace Test1.Controllers
{
    public class MenController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CartService _cartService;

        public MenController(IWebHostEnvironment webHostEnvironment, CartService cartService)
        {
            _webHostEnvironment = webHostEnvironment;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var menProductsPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "men");
            var products = new List<Test1.Models.Product>();  // Explicitly specify namespace

            if (Directory.Exists(menProductsPath))
            {
                var imageFiles = Directory.GetFiles(menProductsPath)
                    .Where(file => file.EndsWith(".jpg") || file.EndsWith(".png") || file.EndsWith(".gif"));

                foreach (var imageFile in imageFiles)
                {
                    products.Add(new Test1.Models.Product  // Explicitly specify namespace
                    {
                        Name = Path.GetFileNameWithoutExtension(imageFile),
                        ImagePath = $"/images/men/{Path.GetFileName(imageFile)}",
                        Price = 99.99M,
                        Category = "Men",
                        Description = "This is a sample description for the product. The admin can update this text.", // This will be replaced with actual description from database
                        IsAvailable = true
                    });
                }
            }

            return View(products);
        }

        public IActionResult Details(string id)
        {
            var menProductsPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "men");
            var imagePath = Path.Combine(menProductsPath, id);
    
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            var product = new Test1.Models.Product  // Explicitly specify namespace
            {
                Name = Path.GetFileNameWithoutExtension(id),
                ImagePath = $"/images/men/{id}",
                Price = 99.99M,
                Category = "Men",
                Description = "Product description goes here...",
                IsAvailable = true,
                Sizes = new[] { "S", "M", "L", "XL" }
            };

            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(string productId)
        {
            try
            {
                if (string.IsNullOrEmpty(productId))
                {
                    return Json(new { success = false, message = "Invalid product" });
                }

                var menProductsPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "men");
                var imagePath = Path.Combine(menProductsPath, productId);

                if (!System.IO.File.Exists(imagePath))
                {
                    return Json(new { success = false, message = "Product not found" });
                }

                var product = new Test1.Models.Product  // Explicitly specify namespace
                {
                    Name = Path.GetFileNameWithoutExtension(productId),
                    ImagePath = $"/images/men/{productId}",
                    Price = 99.99M,
                    Category = "Men",
                    IsAvailable = true
                };

                _cartService.AddToCart(product);

                return Json(new { 
                    success = true, 
                    cartCount = _cartService.GetCartCount(),
                    message = "Product added to cart successfully!"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}