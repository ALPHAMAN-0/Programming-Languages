using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Test1.Models;

namespace Test1.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "Cart";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Models.Product> GetCart()  // Explicitly specify Models.Product
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
            {
                return new List<Models.Product>();
            }

            var cartJson = session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<Models.Product>();
            }

            try
            {
                return JsonSerializer.Deserialize<List<Models.Product>>(cartJson) ?? new List<Models.Product>();
            }
            catch
            {
                return new List<Models.Product>();
            }
        }

        public void AddToCart(Models.Product product)  // Explicitly specify Models.Product
        {
            if (product == null)
            {
                return;
            }

            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null)
            {
                return;
            }

            var cart = GetCart();
            cart.Add(product);

            try
            {
                var cartJson = JsonSerializer.Serialize(cart);
                session.SetString(CartSessionKey, cartJson);
            }
            catch
            {
                // Log error if needed
            }
        }

        public int GetCartCount()
        {
            try
            {
                var cart = GetCart();
                return cart?.Count ?? 0;
            }
            catch
            {
                return 0;
            }
        }
    }

    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public string? Description { get; set; }
    }
}