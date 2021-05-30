using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface IOrderProcessorService
    {
        Task ProcessOrderAsync(IEnumerable<CartLineDTO> cart, ShippingDetailsDTO shippingDetails);
        Task ProccesOrderAuthenticatedAsync(IEnumerable<CartLineDTO> cart, ShippingDetailsDTO shippingDetails, string userId);
        int GetSalesPerson(string country);

    }
}
