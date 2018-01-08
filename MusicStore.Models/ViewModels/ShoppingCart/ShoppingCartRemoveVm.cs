using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore.Models.ViewModels.ShoppingCart
{
    public class ShoppingCartRemoveVm
    {
        public string Message { get; set; }
        public decimal CartTotal { get; set; }
        public int CartCount { get; set; }
        public int ItemCount { get; set; }
        public int DeleteId { get; set; }
    }
}
