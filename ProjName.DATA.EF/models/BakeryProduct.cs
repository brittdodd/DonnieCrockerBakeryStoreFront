using System;
using System.Collections.Generic;

namespace ProjName.DATA.EF.Models
{
    public partial class BakeryProduct
    {
        public BakeryProduct()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string BakeryItems { get; set; } = null!;
        public string QuantityPerUnit { get; set; } = null!;
        public decimal Price { get; set; }
        public bool? Discontinued { get; set; }
        public int SeaonalId { get; set; }
        public string? ProductImage { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Season Seaonal { get; set; } = null!;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
