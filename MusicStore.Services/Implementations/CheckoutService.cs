using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore.Services.Implementations
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.Entity;
    using Models.ViewModels.Order;

    public class CheckoutService : ICheckoutService
    {
        private readonly MusicStoreDbContext _db;
        private readonly IMapper _mapper;

        public CheckoutService(MusicStoreDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        public async Task ProcessOrder(ShoppingCart cart, OrderInputVm order)
        {
            var mapOrder = this._mapper.Map<Order>(order);

            this._db.Orders.Add(mapOrder);
            await cart.CreateOrder(mapOrder);

            await this._db.SaveChangesAsync();
        }
    }
}
