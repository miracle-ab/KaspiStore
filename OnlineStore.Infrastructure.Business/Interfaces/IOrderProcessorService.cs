using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Services;
using System.Collections.Generic;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface IOrderProcessorService
    {
        void ProcessOrder(IEnumerable<CartLineDTO> cart, ShippingDetailsDTO shippingDetails);
        void ProccesOrderAuthenticated(IEnumerable<CartLineDTO> cart, ShippingDetailsDTO shippingDetails, string userId);
        int GetSalesPerson(string country);

    }
}
