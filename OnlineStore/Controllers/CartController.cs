using System.Web.Mvc;
using OnlineStore.Models.ViewModels;
using System.Linq;
using OnlineStore.Infrastructure.Business.DTO;
using AutoMapper;
using OnlineStore.ProductServiceReference;
using OnlineStore.OrderProcessorServiceReference;
using OnlineStore.CartStoreServiceReference;
using System.Collections.Generic;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        IProductSVC productService;
        IOrderProcessorSVC orderProcessorService;

        public CartController(IProductSVC prodServ, IOrderProcessorSVC orderProcServ)
        {
            productService = prodServ;
            orderProcessorService = orderProcServ;
        }

        public ViewResult Index(CartSVCClient cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(CartSVCClient cart, int productId, string returnUrl)
        {
            var productDTO = productService.GetProduct(productId);

            if (productDTO != null)
            {
                cart.AddItem(productDTO, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(CartSVCClient cart, int productId, string returnUrl)
        {
            var productDTO = productService.GetProduct(productId);

            if (productDTO != null)
            {
                cart.RemoveLine(productDTO);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public int ComputeTotalQuantity(CartSVCClient cart)
        {
            return cart.ComputeTotalQuantity();
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetailsViewModel());
        }

        [HttpPost]
        public ViewResult Checkout(CartSVCClient cart, ShippingDetailsViewModel shippingDetails)
        {
            if (cart.Lines().Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ShippingDetailsViewModel, ShippingDetailsDTO>()).CreateMapper();
                var shippingDetailsDTO = mapper.Map<ShippingDetailsViewModel, ShippingDetailsDTO>(shippingDetails);

                CartLineDTO[] cartLines = cart.Lines();

                orderProcessorService.ProcessOrder(cartLines, shippingDetailsDTO);
                cart.Clear();

                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}
