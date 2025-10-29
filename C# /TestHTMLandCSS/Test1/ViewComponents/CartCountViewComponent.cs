using Microsoft.AspNetCore.Mvc;
using Test1.Services;

namespace Test1.ViewComponents
{
    public class CartCountViewComponent : ViewComponent
    {
        private readonly CartService _cartService;

        public CartCountViewComponent(CartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cartService.GetCartCount());
        }
    }
}