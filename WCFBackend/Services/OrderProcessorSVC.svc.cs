using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Interfaces;
using OnlineStore.Infrastructure.Business.Services;
using System.Collections.Generic;

namespace WCFBackend
{
    public class OrderProcessorSVC : IOrderProcessorSVC
    {
        IOrderProcessorService orderProcessorService;
        public OrderProcessorSVC(IOrderProcessorService orderPrServ)
        {
            orderProcessorService = orderPrServ;
        }

        public void ProccesOrderAuthenticated(IEnumerable<CartLineDTO> cart, ShippingDetailsDTO shippingDetails, string userId)
        {
            orderProcessorService.ProccesOrderAuthenticatedAsync(cart, shippingDetails, userId);
        }

        public void ProcessOrder(IEnumerable<CartLineDTO> cart, ShippingDetailsDTO shippingDetails)
        {
            orderProcessorService.ProcessOrderAsync(cart, shippingDetails);
        }
    }
}
