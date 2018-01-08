using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore.Models.ViewModels.ShoppingCart
{
    using Entity;

    public class ShoppingCartVm
    {
        public IEnumerable<CartItem> CartItems { get; set; }

        public decimal CartTotal { get; set; }
    }
}
