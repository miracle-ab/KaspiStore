using OnlineStore.Infrastructure.Business.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface ISalesPersonService
    {
        IQueryable<OrderHeaderDTO> GetOrderHeaders(string userId);
        IQueryable<OrderHeaderDTO> GetAllOrderHeaders();
        SalesProductDTO GetOrderDetails(int purchaseOrderHeaderID);
        Task CreateShipmentXMLAsync(int purchaseOrderHeaderID);
    }
}
