using System;
using System.Collections.Generic;

namespace ProjName.DATA.EF.models
{
    public partial class Season
    {
        public Season()
        {
            BakeryProducts = new HashSet<BakeryProduct>();
        }

        public int SeasonId { get; set; }
        public string SeasonName { get; set; } = null!;

        public virtual ICollection<BakeryProduct> BakeryProducts { get; set; }
    }
}
