using OnlineStore.Infrastructure.Business.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace WCFBackend
{
    [ServiceContract]
    public interface IOrderProcessorSVC
    {
        [OperationContract]
        void ProcessOrder(IEnumerable<CartLineDTO> cart, ShippingDetailsDTO shippingDetails);
    }
}
