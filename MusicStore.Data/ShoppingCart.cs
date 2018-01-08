using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Models.Entity;

    public class ShoppingCart
    {
        private readonly MusicStoreDbContext _db;
        private readonly string _shoppingCartId;

        public ShoppingCart(MusicStoreDbContext db, string id)
        {
            this._db = db;
            this._shoppingCartId = id;
        }

        public static ShoppingCart GetCart(MusicStoreDbContext db, HttpContext context)
            => GetCart(db, GetCartId(context));

        public static ShoppingCart GetCart(MusicStoreDbContext db, string cartId)
            => new ShoppingCart(db, cartId);

        public async Task AddToCart(Album album)
        {
            var cartItem = await this._db.CartItems.SingleOrDefaultAsync(
                c => c.CartId == _shoppingCartId
                     && c.AlbumId == album.AlbumId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    AlbumId = album.AlbumId,
                    CartId = _shoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                this._db.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = this._db.CartItems.SingleOrDefault(
                cart => cart.CartId == _shoppingCartId
                        && cart.CartItemId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    this._db.CartItems.Remove(cartItem);
                }

            }
            return itemCount;
        }

        public async Task EmptyCart()
        {
            var cartItems = await this._db
                .CartItems
                .Where(cart => cart.CartId == _shoppingCartId)
                .ToArrayAsync();

            this._db.CartItems.RemoveRange(cartItems);
        }

        public Task<List<CartItem>> GetCartItems()
        {
            return this._db
                .CartItems
                .Where(cart => cart.CartId == _shoppingCartId)
                .Include(c => c.Album)
                .ToListAsync();

        }

        public Task<List<string>> GetCartAlbumTitles()
        {
            return this._db
                .CartItems
                .Where(cart => cart.CartId == _shoppingCartId)
                .Select(c => c.Album.Title)
                .OrderBy(n => n)
                .ToListAsync();
        }

        public Task<int> GetCount()
        {
            return this._db
                .CartItems
                .Where(c => c.CartId == _shoppingCartId)
                .Select(c => c.Count)
                .SumAsync();
        }

        public Task<decimal> GetTotal()
        {
            return this._db
                .CartItems
                .Where(c => c.CartId == _shoppingCartId)
                .Select(c => c.Album.Price * c.Count)
                .SumAsync();
        }

        public async Task<int> CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            var cartItems = await GetCartItems();

            foreach (var item in cartItems)
            {
                var album = await this._db.Albums.SingleAsync(a => a.AlbumId == item.AlbumId);

                var orderDetail = new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    OrderId = order.OrderId,
                    UnitPrice = album.Price,
                    Quantity = item.Count
                };

                orderTotal += (item.Count * album.Price);

                this._db.OrderDetails.Add(orderDetail);
            }

            order.Total = orderTotal;

            await EmptyCart();

            return order.OrderId;
        }

        private static string GetCartId(HttpContext context)
        {
            var cartId = context.Session.GetString("Session");

            if (cartId == null)
            {
                //A GUID to hold the cartId. 
                cartId = Guid.NewGuid().ToString();

                // Send cart Id as a cookie to the client.
                context.Session.SetString("Session", cartId);
            }

            return cartId;
        }
    }
}
