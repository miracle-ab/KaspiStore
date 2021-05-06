using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Infrastructure.Data;
using OnlineStore.Models.ViewModels;
using OnlineStore.Domain.Core.Entities;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork unitOfWork;
        public HomeController()
        {
            unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {

            var products = (from p in unitOfWork.Products.GetList()
                         join prodInvent in unitOfWork.ProductInventory.GetList() on p.ProductID equals prodInvent.ProductID
                         join prodProdPh in unitOfWork.ProductProductPhoto.GetList() on p.ProductID equals prodProdPh.ProductID
                         join prodPh in unitOfWork.ProductPhoto.GetList() on prodProdPh.ProductPhotoID equals prodPh.ProductPhotoID
                         join prodMod in unitOfWork.ProductModel.GetList() on p.ProductModelID equals prodMod.ProductModelID
                         join prodDescCul in unitOfWork.ProductDescriptionCulture.GetList() on prodMod.ProductModelID equals prodDescCul.ProductModelID
                         join prodDesc in unitOfWork.ProductDescription.GetList() on prodDescCul.ProductDescriptionID equals prodDesc.ProductDescriptionID   
                         where prodInvent.Quantity > 0 && prodDescCul.CultureID.Contains("en")
                         select new ProductList {
                             ProductID = p.ProductID,
                             Name = p.Name,
                             Color = p.Color,
                             Description = prodDesc.Description,
                             Photo = prodPh.LargePhoto,
                             Price = p.ListPrice
                         }).Distinct().Take(12);


            return View(new ProductsViewModel { Products = products });
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}