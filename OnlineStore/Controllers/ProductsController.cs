using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Infrastructure.Data;
using OnlineStore.Models.ViewModels;

namespace OnlineStore.Controllers
{
    public class ProductsController : Controller
    {

        UnitOfWork unitOfWork;
        public ProductsController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: Products
        public ActionResult Index(int page = 1)
        {
            int pageSize = 24;

            var products = (from p in unitOfWork.Products.GetList()
                            join prodInvent in unitOfWork.ProductInventory.GetList() on p.ProductID equals prodInvent.ProductID
                            join prodProdPh in unitOfWork.ProductProductPhoto.GetList() on p.ProductID equals prodProdPh.ProductID
                            join prodPh in unitOfWork.ProductPhoto.GetList() on prodProdPh.ProductPhotoID equals prodPh.ProductPhotoID
                            join prodMod in unitOfWork.ProductModel.GetList() on p.ProductModelID equals prodMod.ProductModelID
                            join prodDescCul in unitOfWork.ProductDescriptionCulture.GetList() on prodMod.ProductModelID equals prodDescCul.ProductModelID
                            join prodDesc in unitOfWork.ProductDescription.GetList() on prodDescCul.ProductDescriptionID equals prodDesc.ProductDescriptionID
                            where prodInvent.Quantity > 0 && prodDescCul.CultureID.Contains("en")
                            select new ProductList
                            {
                                ProductID = p.ProductID,
                                Name = p.Name,
                                Color = p.Color,
                                Description = prodDesc.Description,
                                Photo = prodPh.LargePhoto,
                                Price = p.ListPrice
                            }).Distinct();

            var productsPerPages = products
                                    .OrderBy(i => i.ProductID)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize);

            PageInfo pageInfo = new PageInfo { 
                PageNumber = page, 
                PageSize = pageSize, 
                TotalItems = products.Count() 
            };

            return View(new ProductsViewModel { Products = productsPerPages, PageInfo = pageInfo });
        }

        public ActionResult Details(int id)
        {
            var product = unitOfWork.Products.Get(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            string description;

            if (product.ProductModel != null)
            {
                var descCultureEN = product.ProductModel.ProductModelProductDescriptionCultures.First(i => i.CultureID.Contains("en"));

                description = unitOfWork.ProductDescription.Get(descCultureEN.ProductDescriptionID).Description;
            } else
            {
                description = "No description";
            }

            
            var photo = product.ProductProductPhotoes.First();
            string base64 = Convert.ToBase64String(photo.ProductPhoto.LargePhoto);
            string imgSrc = string.Format("data:image/gif;base64,{0}", base64);
       

            var prodView = new ProductViewModel {
                Name = product.Name,
                Color = product.Color,
                ProductNumber = product.ProductNumber,
                ListPrice = product.ListPrice,
                Size = product.Size,
                SizeUnitMeasureCode = product.SizeUnitMeasureCode,
                Description = description,
                Photo = imgSrc
            };

            return View(prodView);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}