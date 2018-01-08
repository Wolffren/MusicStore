namespace MusicStore.Services.Implementations

{
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.ViewModels.ShoppingCart;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly MusicStoreDbContext _db;

        public ShoppingCartService(MusicStoreDbContext db)
        {
            this._db = db;
        }

        public async Task<ShoppingCartVm> GetShoppingCart(ShoppingCart cart)
        {
            
            var result = new ShoppingCartVm
            {
                CartItems = await cart.GetCartItems(),
                CartTotal = await cart.GetTotal()
            };
            return result;
        }

        public async Task AddAlbum(int id, ShoppingCart cart)
        {
            var al = await this._db.Albums.SingleAsync(album => album.AlbumId == id);

            await cart.AddToCart(al);

            await this._db.SaveChangesAsync();
        }

        public async Task<ShoppingCartRemoveVm> GetShoppingCartRemoved(int id, ShoppingCart cart)
        {
          

            var cartItem = await this._db.CartItems
                .Where(item => item.CartItemId == id)
                .Include(c => c.Album)
                .SingleOrDefaultAsync();

            string message;
            int itemCount;

            if (cartItem != null)
            {
                itemCount = cart.RemoveFromCart(id);

                await this._db.SaveChangesAsync();

                string removed = (itemCount > 0) ? "1 copy of " : string.Empty;
                message = removed + cartItem.Album.Title + " has been removed from your shopping cart";
            }
            else
            {
                itemCount = 0;
                message = "Could not find this item, nothing has been removed from your shopping cart.";
            }

            var result = new ShoppingCartRemoveVm
            {
                Message = message,
                CartTotal =  await cart.GetTotal(),
                CartCount = await cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return result;
        }
    }

}