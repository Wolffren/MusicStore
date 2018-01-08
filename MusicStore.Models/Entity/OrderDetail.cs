

namespace MusicStore.Models.Entity
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int AlbumId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public Album Album { get; set; }

        public Order Order { get; set; }
    }
}
