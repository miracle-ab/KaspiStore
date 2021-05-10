using OnlineStore.Infrastructure.Business.Services;

namespace OnlineStore.Models.ViewModels
{
    public class CartIndexViewModel
    {
        public CartService Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
