using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore.Services.Interfaces
{
    using System.Threading.Tasks;
    using Data;
    using Models.ViewModels.Order;

    public interface ICheckoutService
    {
        Task ProcessOrder(ShoppingCart cart,OrderInputVm order);
        Task<bool> CheckIfValid(int id, string userName);
    }
}
