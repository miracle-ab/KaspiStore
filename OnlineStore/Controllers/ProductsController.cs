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

            var products = unitOfWork.Products.GetList(i => i.ProductInventories.FirstOrDefault(prodInvent => prodInvent.Quantity > 0).Quantity > 0);

            var productsPerPages = products.OrderBy(i => i.ProductID).Skip((page - 1) * pageSize).Take(pageSize);

            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = products.Count() };

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
    }
}