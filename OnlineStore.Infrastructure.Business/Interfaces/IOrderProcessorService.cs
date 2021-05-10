using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Services;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface IOrderProcessorService
    {
        void ProcessOrder(CartService cart, ShippingDetailsDTO shippingDetails);

    }
}
