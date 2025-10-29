using Microsoft.AspNetCore.Mvc;

public class SaleController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}