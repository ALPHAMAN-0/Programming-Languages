using Microsoft.AspNetCore.Mvc;

public class WomenController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}