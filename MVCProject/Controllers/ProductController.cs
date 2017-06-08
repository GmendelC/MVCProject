using MVCProject.Models.DataBaseProject;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int id)
        {
            try
            {
                Product product;
                using (var ctx = new InternertMarkertEntitesContainer())
                {
                    product = ctx.Products.Find(id);
                    product.SellingUser = ctx.Users.Find(product.SellingId);
                }
                if (product != null)
                    return View(product);
                else
                    return HttpNotFound();
            }
            catch (Exception)
            {

                return HttpNotFound();
            }
        }

        public ActionResult ByTitle(string title)
        {
            Product product;
            try
            {
                using (var ctx = new InternertMarkertEntitesContainer())
                {
                    product = ctx.Products.Where(x => x.Title == title).FirstOrDefault();
                }
                if (product != null)
                    return View(product);
                else
                    return HttpNotFound();
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        public ActionResult _ItemProduct(Product product)
        {
            return PartialView(product);
        }
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Add([Bind(Exclude = "Image1,Image2,Image3")]Product newProduct, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3, bool isEdit)
        {
            try
            {
                newProduct.SellingId = int.Parse(User.Identity.Name);
                newProduct.Date = DateTime.Now;

                newProduct.Image1 = SaveImage(Image1);
                newProduct.Image2 = SaveImage(Image2);
                newProduct.Image3 = SaveImage(Image3);


                if (ModelState.IsValid)
                {
                    if (isEdit)
                    {
                      return Edit (newProduct);
                    }
                    using (var ctx = new InternertMarkertEntitesContainer())
                    {
                        ctx.Products.Add(newProduct);
                        ctx.SaveChanges();
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(newProduct);
                }

                
            }
            catch (Exception e)
            {
                return View(newProduct);
            }
        }
        [Authorize]
        [HttpGet]
        public ActionResult Edit(int productId)
        {
            try
            {
                using (var ctx = new InternertMarkertEntitesContainer())
                {
                    var product = ctx.Products.Find(productId);

                    if (product != null & product.SellingId == int.Parse(User.Identity.Name))
                    {
                        ViewBag.isEdit = true;
                        return View("Add", product);
                    }
                    else
                        return HttpNotFound();
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var ctx = new InternertMarkertEntitesContainer())
                    {
                        var oldProduct = ctx.Products.Find(product.Id);
                        Upadate(oldProduct, product);
                        ctx.SaveChanges();
                    }
                    return RedirectToAction("Index", "Home"); 
                }
                else
                {
                    ViewBag.isEdit = true;
                    return View("Add", product);
                }
            }
            catch (Exception)
            {
                ViewBag.isEdit = true;
                return View("Add", product);
            }
        }

        private void Upadate(Product oldProduct, Product product)
        {
            oldProduct.Date = product.Date;
            oldProduct.Image1 = product.Image1;
            oldProduct.Image2 = product.Image2;
            oldProduct.Image3 = product.Image3;
            oldProduct.Title = product.Title;
            oldProduct.ShortDescription = product.ShortDescription;
            oldProduct.LogDescription = product.LogDescription;
            oldProduct.Price = product.Price;
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            using (var ctx = new InternertMarkertEntitesContainer())
            {
                try
                {

                    Product product = ctx.Products.Find(id);
                    ctx.Products.Remove(product);
                    ctx.SaveChanges();

                    return View(product);
                }

                catch (Exception)
                {
                    return RedirectToAction("UserProducts", "Home", null);
                }

            }
            }

        private static FileEntity SaveImage(HttpPostedFileBase Image)
        {
            FileEntity eImage = new FileEntity();
            if (Image != null)
            {
                eImage.MIME = Image.ContentType;
                eImage.File = new byte[Image.ContentLength];
                Image.InputStream.Read(eImage.File, 0, Image.ContentLength);
            }
            return eImage;
        }

        public ActionResult AddToCar(Product sellProduct)// bool ?? acition result
        {
            try
            {
                if (User.Identity.IsAuthenticated & !sellProduct.InCar & sellProduct.ByingId == null)
                {
                    sellProduct.ByingId = int.Parse(User.Identity.Name);
                    using (var ctx = new InternertMarkertEntitesContainer())
                    {
                        var product = ctx.Products.Find(sellProduct.Id);
                        product.ByingId = sellProduct.ByingId;
                        product.InCar = true;
                        ctx.SaveChanges();
                    }
                    return RedirectToAction("Car", "Home");
                }
                else
                    return RedirectToAction("Car", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Car", "Home");
            }
        }

        public ActionResult RemoveFromCar(int  id)//
        {
            try
            {
                using (var ctx = new InternertMarkertEntitesContainer())
                {
                    var product = ctx.Products.Find(id);
                    if (product.ByingId == int.Parse(User.Identity.Name))
                    {
                        product.ByingId = null;
                        product.InCar = false;
                        ctx.SaveChanges(); 
                    }
                }
            }
            catch(Exception) { }
            return RedirectToAction("Car", "Home");
        }
        public void sell(Product product, InternertMarkertEntitesContainer ctx)
        {
                    var p = ctx.Products.Find(product.Id);
                    p.InCar = false;
        }

        public  FileContentResult GetImage(int id, int target)
        {
            FileEntity image;

            using (var ctx = new InternertMarkertEntitesContainer())
            {
                switch (target)
                {
                    case 1:
                        image = ctx.Products.Find(id).Image1;
                        break;
                    case 2:
                        image = ctx.Products.Find(id).Image2;
                        break;
                    case 3:
                        image = ctx.Products.Find(id).Image3;
                        break;
                    default:
                        image = null;
                        break;
                }
               
            }

            if (image.File != null & image.MIME != null)
                return File(image.File, image.MIME);
            else
                return null;
        }
    }
}