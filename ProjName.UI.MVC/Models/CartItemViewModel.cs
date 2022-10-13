using ProjName.DATA.EF.Models;
using ProjName.UI.MVC.Models;



namespace ProjName.UI.MVC.Models
{
    public class CartItemViewModel
    {

        public int Qty { get; set; }

        public BakeryProduct CartProd { get; set; }

        public CartItemViewModel() { }

        public CartItemViewModel(int qty, BakeryProduct product)
        {
            Qty = qty;
            CartProd = product;
        }

    }
}
