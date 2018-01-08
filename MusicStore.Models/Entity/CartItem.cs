using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore.Models.Entity
{
    using System.ComponentModel.DataAnnotations;

    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required]
        public string CartId { get; set; }
        public int AlbumId { get; set; }
        public int Count { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        public Album Album { get; set; }
    }
}
