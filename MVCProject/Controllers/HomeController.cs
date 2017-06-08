using MVCProject.Models.DataBaseProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            try
            {
                List<Product> products;

            using (var ctx = new InternertMarkertEntitesContainer())
                {
                    products = ctx.Products.OrderBy(x => x.Date).ToList();
                }
                return View(products);
            }
            catch (Exception x)
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult ByTitle()
        {
            try
            {
                List<Product> products;
                using (var ctx = new InternertMarkertEntitesContainer())
                {
                    products = ctx.Products.OrderBy(x => x.Title).ToList();
                }
                return View("Index",products);
            }
            catch (Exception)
            {
                return RedirectToAction("ByTitle");
            }
        }

        public ActionResult Abouht()
        {
            return View();
        }

        [Authorize]
        public ActionResult Car()
        {
            try
            {
                List<Product> carProduct;
                int id = int.Parse(User.Identity.Name);


                using (var ctx = new InternertMarkertEntitesContainer())
                {
                    carProduct = ctx.Products
                        .Where(x => x.ByingId == id& x.InCar)
                        .OrderBy(x => x.Title).ToList();
                }

                return View(carProduct);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }
        [Authorize]
        public ActionResult UserProducts()
        {
            try
            {
                List<Product> Products;
                using (var ctx = new InternertMarkertEntitesContainer())
                {
                    Products = ctx.Products
                        .Where(x => x.SellingId == int.Parse(User.Identity.Name))
                        .OrderBy(x => x.Title).ToList();
                }
                return View(Products);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Sell()
        {
            try
            {
                using (var ctx = new InternertMarkertEntitesContainer())
                {
                    var products = ctx.Products.Where(x => x.ByingId.Value.ToString() == User.Identity.Name);

                    foreach (var item in products)
                    {
                        RedirectToAction("Sell", "Product", item);
                    }
                }
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Car");
            }
        }
    }
}