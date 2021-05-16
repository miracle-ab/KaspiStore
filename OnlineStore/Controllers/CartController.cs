using System.Web.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Infrastructure.Business.Interfaces;
using OnlineStore.Infrastructure.Business.Services;
using System.Linq;
using OnlineStore.Infrastructure.Business.DTO;
using AutoMapper;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        IProductService productService;
        IOrderProcessorService orderProcessorService;

        public CartController(IProductService prodServ, IOrderProcessorService orderPrServ)
        {
            productService = prodServ;
            orderProcessorService = orderPrServ;
        }

        public ViewResult Index(CartService cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(CartService cart, int productId, string returnUrl)
        {
            var productDTO = productService.GetProduct(productId);

            if (productDTO != null)
            {
                cart.AddItem(productDTO, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(CartService cart, int productId, string returnUrl)
        {
            var productDTO = productService.GetProduct(productId);

            if (productDTO != null)
            {
                cart.RemoveLine(productDTO);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public int ComputeTotalQuantity(CartService cart)
        {
            return cart.ComputeTotalQuantity();
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetailsViewModel());
        }

        [HttpPost]
        public ViewResult Checkout(CartService cart, ShippingDetailsViewModel shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ShippingDetailsViewModel, ShippingDetailsDTO>()).CreateMapper();
                var shippingDetailsDTO = mapper.Map<ShippingDetailsViewModel, ShippingDetailsDTO>(shippingDetails);

                orderProcessorService.ProcessOrder(cart, shippingDetailsDTO);
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
