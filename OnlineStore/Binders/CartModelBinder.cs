using OnlineStore.CartStoreServiceReference;
using OnlineStore.Infrastructure.Business.Services;
using System.Web.Mvc;

namespace OnlineStore.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            CartSVCClient cart = null;

            if (controllerContext.HttpContext.Session != null)
            {
                cart = (CartSVCClient)controllerContext.HttpContext.Session[sessionKey];
            }

            if (cart == null)
            {
                cart = new CartSVCClient();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }

            return cart;
        }
    }
}
