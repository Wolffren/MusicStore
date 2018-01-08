
namespace MusicStore.Web.Controllers
{
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using MusicStore.Services.Implementations;
    using MusicStore.Services.Interfaces;

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _service;
        private readonly MusicStoreDbContext _db;
        public ShoppingCartController(IShoppingCartService service,MusicStoreDbContext db)
        {
            this._service = service;
            this._db = db;
        }
        public async Task<IActionResult> Index()
        {
            var shoppingCart = ShoppingCart.GetCart(_db, HttpContext);
            var cart = await this._service.GetShoppingCart(shoppingCart);
            return View(cart);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var cart = ShoppingCart.GetCart(this._db, HttpContext);
            await this._service.AddAlbum(id,cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this._db, HttpContext);
            var shoppingCartRemove = await this._service.GetShoppingCartRemoved(id,cart);

            return Json(shoppingCartRemove);
        }
    }
}