using System.Web.Mvc;
using OnlineStore.Models.ViewModels;
using OnlineStore.Infrastructure.Business.Interfaces;
using OnlineStore.Infrastructure.Business.Services;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        IProductService productService;
        public CartController(IProductService serv)
        {
            productService = serv;
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

        public ViewResult Checkout(CartService cart, ShippingDetailsViewModel shippingDetails)
        {
            return View(new ShippingDetailsViewModel());
        }
    }
}
