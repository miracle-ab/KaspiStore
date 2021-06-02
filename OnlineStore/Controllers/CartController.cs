using System.Web.Mvc;
using OnlineStore.Models.ViewModels;
using System.Linq;
using OnlineStore.Infrastructure.Business.DTO;
using AutoMapper;
using OnlineStore.ProductServiceReference;
using OnlineStore.OrderProcessorServiceReference;
using OnlineStore.CartStoreServiceReference;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

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
        public RedirectToRouteResult SubstractItemCart(CartSVCClient cart, int productId, string returnUrl)
        {
            var productDTO = productService.GetProduct(productId);

            if (productDTO != null)
            {
                cart.SubstractQtyItem(productDTO, 1);
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

        [Authorize(Roles = "user")]
        public ViewResult CheckoutAuthenticated()
        {
            return View(new ShippingDetailsAuthenticatedViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Checkout(CartSVCClient cart, ShippingDetailsViewModel shippingDetails)
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

                await orderProcessorService.ProcessOrderAsync(cartLines, shippingDetailsDTO);
                cart.Clear();

                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult> CheckoutAuthenticated(CartSVCClient cart, ShippingDetailsAuthenticatedViewModel shippingDetails)
        {
            if (cart.Lines().Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ShippingDetailsAuthenticatedViewModel, ShippingDetailsDTO>()).CreateMapper();
                var shippingDetailsDTO = mapper.Map<ShippingDetailsAuthenticatedViewModel, ShippingDetailsDTO>(shippingDetails);

                CartLineDTO[] cartLines = cart.Lines();

                await orderProcessorService.ProccesOrderAuthenticatedAsync(cartLines, shippingDetailsDTO, userId);
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
