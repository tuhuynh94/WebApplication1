using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Infrastructure;
using WebApplication1.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        // GET: /<controller>/
        private ISongRepository _repository;
        private Cart _cart;
        public CartController(ISongRepository repo, Cart cart)
        {
            _repository = repo;
            _cart = cart;

        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        public async Task<RedirectToActionResult> AddToCart(int id, string returnUrl)
        {
            SongModel product = await _repository.SearchAsync(id);
            if (product != null)
            {
               
                _cart.AddLineItem(product, 1);
                
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public async Task<RedirectToActionResult> RemoveFromCart(int productId,
        string returnUrl)
        {
            SongModel product = await _repository.SearchAsync(productId);
            if (product != null)
            {
                _cart.RemoveLineItem(product);
      
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        //private Cart GetCart()
        //{
        //    Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        //    return cart;
        //}
        //private void SaveCart(Cart cart)
        //{
        //    HttpContext.Session.SetJson("Cart", cart);
        //}
    }
}
