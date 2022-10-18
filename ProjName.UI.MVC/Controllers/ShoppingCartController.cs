﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjName.UI.MVC.Models;
using ProjName.DATA.EF.Models;
using Newtonsoft.Json;

namespace ProjName.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly StoreFrontContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ShoppingCartController(StoreFrontContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            //retrieve the cart
            var sessionCart = HttpContext.Session.GetString("cart");

            //create the shell for the local (C#) shopping cart
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            if (sessionCart == null || sessionCart.Count() == 0)
            {
                ViewBag.Message = "There are no items in your cart.";

                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }
            else
            {
                ViewBag.Message = null;

                shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);
            }

            return View(shoppingCart);
        }

        public IActionResult AddToCart(int id)
        {
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            var sessionCart = HttpContext.Session.GetString("cart");

            if (sessionCart == null)
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }
            else
            {
                shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);
            }

            BakeryProduct product = _context.BakeryProducts.Find(id);

            CartItemViewModel civm = new CartItemViewModel(1, product);

            if (shoppingCart.ContainsKey(product.ProductId))
            {
                
                shoppingCart[product.ProductId].Qty++;
            }
            else
            {
                
                shoppingCart.Add(product.ProductId, civm);
            }

            string jsonCart = JsonConvert.SerializeObject(shoppingCart);
            HttpContext.Session.SetString("cart", jsonCart);

            return RedirectToAction("Index");
        }


        public IActionResult RemoveFromCart(int id)
        {
            //retrieve our cart from session
            var sessionCart = HttpContext.Session.GetString("cart");

            // convert JSON cart to C#
            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            //Remove the cart item from the C# collection
            shoppingCart.Remove(id);

            //update session again
            // - if there are no items remaining in the cart, remove the cart from session
            if (shoppingCart.Count == 0)
            {
                HttpContext.Session.Remove("cart");
            }
            // - otherwise, update the session variable with our local cart contents
            else
            {
                string jsonCart = JsonConvert.SerializeObject(shoppingCart);
                HttpContext.Session.SetString("cart", jsonCart);
            }

            return RedirectToAction("Index");

        }


        public IActionResult UpdateCart(int productId, int qty)
        {
            
            var sessionCart = HttpContext.Session.GetString("cart");

            
            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            
            shoppingCart[productId].Qty = qty;

            
            string jsonCart = JsonConvert.SerializeObject(shoppingCart);
            HttpContext.Session.SetString("cart", jsonCart);


            return RedirectToAction("Index");
        }

        //This method MUST be async in order to invoke the UserManager's async methods in this action.
        public async Task<IActionResult> SubmitOrder()
        {
            #region Planning out Order Submission
            //Create Order object -> Then save to DB
            // - UserId (get from Identity) 
            // - OrderDate (Current date/time aka DateTime.Now)
            // - ShipToName -- the person who is ordering (UserDetails)
            // - ShipCity (UserDetails)
            // - ShipState (UserDetails)
            // - ShipZip (UserDetails)
            // Add the record to _context
            // Save DB Changes


            //Create OrderProduct objects for each item in the cart -> Then save to the DB
            // - ProductId (Cart)
            // - OrderId (Order object)
            // - Quantity (Cart)
            // - ProductPrice (Cart)
            // Add the record to _context
            // Save DB Changes

            #endregion

            // Retrieve current user's Id
            string? userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;

            //Retrieve the UserDetails record from the DB
            UserDetail ud = _context.UserDetails.Find(userId);

            // Create the Order object
            Order o = new Order()
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                ShipToName = ud.FullName,
                ShipCity = ud.City,
                ShipState = ud.State,
                ShipZip = ud.Zip
            };

            // Add the order to _context
            _context.Orders.Add(o);

            // Retrieve the session cart and convert to C#
            var sessionCart = HttpContext.Session.GetString("cart");
            Dictionary<int, CartItemViewModel> shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);

            //Create an OrderProduct record for every Product in our cart
            foreach (var item in shoppingCart)
            {
                OrderProduct op = new OrderProduct()
                {
                    OrderId = o.OrderId,
                    ProductId = item.Value.CartProd.ProductId,
                    ProductPrice = item.Value.CartProd.Price,
                    Quantity = (short?)item.Value.Qty
                };

                //ONLY need to add items to an existing entity (here -> the order 'o') if the items are a related record (like the OrderProduct here)
                o.OrderProducts.Add(op);
            }

            //Save changes to DB
            _context.SaveChanges();

            // Now that the order has been saved to the database, we can empty the cart.
            HttpContext.Session.Remove("cart");

            return RedirectToAction("Index", "Orders");
        }
    }
}
