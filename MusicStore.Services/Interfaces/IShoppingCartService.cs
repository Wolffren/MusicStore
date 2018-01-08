namespace MusicStore.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Http;
    using Models.Entity;
    using Models.ViewModels.Album;
    using Models.ViewModels.ShoppingCart;

    public interface IShoppingCartService
    {
        Task<ShoppingCartVm> GetShoppingCart(ShoppingCart cart);

        Task AddAlbum(int id, ShoppingCart cart);

        Task<ShoppingCartRemoveVm> GetShoppingCartRemoved(int id, ShoppingCart cart);

    }
}
