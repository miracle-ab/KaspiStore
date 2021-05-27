using OnlineStore.Infrastructure.Business.DTO;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface ISalesPersonService
    {
        IQueryable<OrderHeaderDTO> GetOrderHeaders(string userId);
        IQueryable<OrderHeaderDTO> GetAllOrderHeaders();
        SalesProductDTO GetOrderDetails(int purchaseOrderHeaderID);
        void CreateShipmentXML(int purchaseOrderHeaderID);
    }
}
