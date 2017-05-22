using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repository;
        private Cart _cart;
        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            _repository = repoService;
            _cart = cartService;
        }
        [Authorize]
        public async Task<ViewResult> List() =>
                View(await _repository.UnshippedOrdersAsync());

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MarkShipped(int orderID)
        {
            Order order = await _repository.SearchAsync(orderID);
            if (order != null)
            {
                order.Shipped = true;
                 await _repository.SaveOrderAsync(order);
            }
            return RedirectToAction(nameof(List));
        }
        public ViewResult Checkout() => View(new Order() { LineItems = _cart.LineItems});

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (_cart.LineItems.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.LineItems = _cart.LineItems;
                await _repository.SaveOrderAsync(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}