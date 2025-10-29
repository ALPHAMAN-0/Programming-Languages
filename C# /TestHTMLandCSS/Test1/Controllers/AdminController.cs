using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Test1.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Test1.Controllers
{
    public class AdminController : Controller
    {
        private readonly string _adminEmail = "admin@gearcastle.com";
        private readonly string _adminPassword = "Admin123!";
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AdminLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Email == _adminEmail && model.Password == _adminPassword)
                {
                    HttpContext.Session.SetString("IsAdmin", "true");
                    return RedirectToAction("Dashboard");
                }
                ModelState.AddModelError("", "Invalid login attempt");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Products()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductUploadModel model)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                if (ModelState.IsValid && model.Image != null)
                {
                    // Check file size (e.g., max 10MB)
                    if (model.Image.Length > 10 * 1024 * 1024)
                    {
                        TempData["Error"] = "File size must be less than 10MB";
                        return RedirectToAction("Products");
                    }

                    // Check file type
                    var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                    if (!allowedTypes.Contains(model.Image.ContentType.ToLower()))
                    {
                        TempData["Error"] = "Only .jpg, .png and .gif files are allowed";
                        return RedirectToAction("Products");
                    }

                    // Map category names to folder names
                    var categoryFolder = model.Category.ToLower() switch
                    {
                        "men" => "men",
                        "women" => "women",
                        "newborn" => "newborn",
                        _ => throw new ArgumentException("Invalid category")
                    };

                    // Create category-specific directory path
                    var categoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", categoryFolder);
                    
                    // Create the directory if it doesn't exist
                    Directory.CreateDirectory(categoryPath);

                    // Generate unique filename
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.Image.FileName)}";
                    var filePath = Path.Combine(categoryPath, fileName);

                    // Save the image
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }

                    // Save the relative path with forward slashes
                    var relativePath = $"images/{categoryFolder}/{fileName}";

                    TempData["Success"] = "Product added successfully!";
                    return RedirectToAction("Products");
                }

                TempData["Error"] = "Please check your input and try again.";
                return RedirectToAction("Products");
            }
            catch (Exception ex)
            {
                // Log the exception details
                TempData["Error"] = $"Error uploading product: {ex.Message}";
                return RedirectToAction("Products");
            }
        }

        public IActionResult ManageProducts()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }

            var products = new List<Product>();
            var categories = new[] { "men", "women", "newborn" };

            foreach (var category in categories)
            {
                var categoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", category);
                if (Directory.Exists(categoryPath))
                {
                    var imageFiles = Directory.GetFiles(categoryPath)
                        .Where(file => file.EndsWith(".jpg") || file.EndsWith(".png") || file.EndsWith(".gif"));

                    foreach (var imageFile in imageFiles)
                    {
                        products.Add(new Product
                        {
                            Name = Path.GetFileNameWithoutExtension(imageFile),
                            Category = category,
                            ImagePath = $"/images/{category}/{Path.GetFileName(imageFile)}",
                            Price = 99.99M // Default price
                        });
                    }
                }
            }

            return View(products);
        }

        [HttpPost]
        public IActionResult DeleteProduct(string imagePath)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }

            try
            {
                if (string.IsNullOrEmpty(imagePath))
                {
                    TempData["Error"] = "Invalid image path";
                    return RedirectToAction("ManageProducts");
                }

                // Clean up the path and make it relative to wwwroot
                imagePath = imagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    TempData["Success"] = "Product deleted successfully";
                }
                else
                {
                    TempData["Error"] = "File not found";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error deleting product: {ex.Message}";
            }

            return RedirectToAction("ManageProducts");
        }
    }
}

