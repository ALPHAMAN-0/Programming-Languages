using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Test1.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

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

            if (ModelState.IsValid && model.Image != null && model.Image.Length > 0)
            {
                try
                {
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
                    if (!Directory.Exists(categoryPath))
                    {
                        Directory.CreateDirectory(categoryPath);
                    }

                    // Generate unique filename
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.Image.FileName)}";
                    var filePath = Path.Combine(categoryPath, fileName);

                    // Save the image
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }

                    // Save the relative path in the database
                    var relativePath = Path.Combine("images", categoryFolder, fileName).Replace("\\", "/");
                    
                    // TODO: Save product to database with relativePath as ImagePath

                    TempData["Success"] = "Product added successfully!";
                    return RedirectToAction("Products");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Error uploading product: {ex.Message}";
                }
            }

            TempData["Error"] = "Please check your input and try again.";
            return RedirectToAction("Products");
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            // TODO: Implement delete logic
            return RedirectToAction("Products");
        }
    }
}

