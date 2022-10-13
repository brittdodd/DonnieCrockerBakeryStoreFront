using System;
using System.Collections.Generic;

namespace ProjName.UI.MVC.models
{
    public partial class Category
    {
        public Category()
        {
            BakeryProducts = new HashSet<BakeryProduct>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public string? Picture { get; set; }

        public virtual ICollection<BakeryProduct> BakeryProducts { get; set; }
    }
}
