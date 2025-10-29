using Microsoft.AspNetCore.Mvc;
using Test1.Models;

public class AccountController : Controller
{
    private readonly string _adminEmail = "admin@gearcastle.com";
    private readonly string _adminPassword = "Admin123!";

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(AdminLoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.Email == _adminEmail && model.Password == _adminPassword)
        {
            HttpContext.Session.SetString("IsAdmin", "true");
            return RedirectToAction("Dashboard", "Admin");
        }

        // Regular user login logic here
        ModelState.AddModelError("", "Invalid login credentials");
        return View(model);
    }

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}