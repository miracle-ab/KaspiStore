using OnlineStore.CartStoreServiceReference;
using OnlineStore.Infrastructure.Business.Services;

namespace OnlineStore.Models.ViewModels
{
    public class CartIndexViewModel
    {
        public CartSVCClient Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
