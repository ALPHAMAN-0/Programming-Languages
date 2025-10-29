using Microsoft.AspNetCore.Mvc;

public class MenController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}