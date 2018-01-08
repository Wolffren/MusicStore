using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MusicStore.Web.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using Models.Entity;
    using Models.ViewModels.Order;
    using MusicStore.Services.Interfaces;

    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _service;
        private readonly MusicStoreDbContext _db;
        public CheckoutController(ICheckoutService service, MusicStoreDbContext db)
        {
            this._service = service;
            this._db = db;
        }

        //Get: /Checkout/AdressAndPayment

        public IActionResult AddressAndPayment()
        {
            return View();
        }

        //Post: /Checkout/AdressAndPayment

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddressAndPayment(OrderInputVm order)
        { 
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            order.Username = HttpContext.User.Identity.Name;
            order.OrderDate = DateTime.Now;

            var cart = ShoppingCart.GetCart(this._db, HttpContext);
 
            await this._service.ProcessOrder(cart,order);

            return RedirectToAction("Complete", new {id = order.OrderId});
        }

        //Get: /Checkout/Complete

        public  IActionResult Complete(int id)
        {
            return View(id);

        }
    }
}