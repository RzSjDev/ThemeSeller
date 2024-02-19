using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace ThemeSeller.Controllers
{
    public class ShopThemeController:Controller
    {
        MyCmsContext db = new MyCmsContext();

        public ActionResult Index()
        {
            List<ShopCartViewModel> list = new List<ShopCartViewModel>();

            if (Session["ShopCart"] != null)
            {
                List<Shopcart> cart = Session["ShopCart"] as List<Shopcart>;
                foreach (var shopCartItem in cart)
                {
                    var theme = db.Theme.Find(shopCartItem.ProductID);
                    list.Add(new ShopCartViewModel()
                    {
                        ProductID = shopCartItem.ProductID,
                        Count = shopCartItem.Count,
                        Title = theme.ThemeTitle,
                        Price = theme.Price,
                        Sum = shopCartItem.Count * theme.Price,
                        ImageName = theme.ImageName

                    });
                }
            }

            return View(list);
        }

        public int AddToCart(int id)
        {
            List<Shopcart> cart = new List<Shopcart>();

            if (Session["ShopCart"] != null)
            {
                cart = Session["ShopCart"] as List<Shopcart>;
            }

            if (cart.Any(p => p.ProductID == id))
            {
               
                int index = cart.FindIndex(p => p.ProductID == id);
                cart[index].Count += 1;
            }
            else
            {
                cart.Add(new Shopcart()
                {
                    ProductID = id,
                    Count = 1
                });
            }

            Session["ShopCart"] = cart;

            return cart.Sum(p => p.Count);
        }

        public int ShopCartCount()
        {
            int count = 0;

            if (Session["ShopCart"] != null)
            {
                List<Shopcart> cart = Session["ShopCart"] as List<Shopcart>;
                count = cart.Sum(p => p.Count);
            }

            return count;
        }

        public ActionResult RemoveFromCart(int id)
        {
            List<Shopcart> cart = Session["ShopCart"] as List<Shopcart>;
            int index = cart.FindIndex(p => p.ProductID == id);
            cart.RemoveAt(index);
            Session["ShopCart"] = cart;
            return RedirectToAction("Index");
        }
    }
}