using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Infrastructure;
using OnlineStore.Infrastructure.Business.Interfaces;
using OnlineStore.Models.ViewModels;
using OnlineStore.ProductServiceReference;

namespace OnlineStore.Controllers
{
    public class ProductsController : Controller
    {
        IProductSVC productService;
        public ProductsController(IProductSVC serv)
        {
            productService = serv;
        }

        // GET: Products
        public ActionResult Index(int page = 1)
        {
            int pageSize = 26;

            IEnumerable<ProductDTO> productDtos = productService.GetProducts();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var products = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);
            
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

        public ActionResult Category(int categoryID)
        {
            IEnumerable<ProductDTO> productDtos = productService.GetProductsByCategory(categoryID);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var products = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);

            return View(new ProductsViewModel { Products = products });
        }

        public ActionResult Details(int id)
        {
            try
            {
                var productDTO = productService.GetProduct(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
                var product = mapper.Map<ProductDTO, ProductViewModel>(productDTO);

                return View(product);

            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Image(string filename)
        {
            var imgDTO = productService.Image(filename);

            if (imgDTO == null)
                HttpNotFound();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PhotoDTO, PhotoViewModel>()).CreateMapper();
            var img = mapper.Map<PhotoDTO, PhotoViewModel>(imgDTO);

            return File(img.LargePhoto, "image/jpeg");
        }

        protected override void Dispose(bool disposing)
        {
            productService.Dispose();
            base.Dispose(disposing);
        }
    }
}