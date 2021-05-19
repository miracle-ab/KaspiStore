using OnlineStore.Infrastructure.Business.DTO;
using System.Linq;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface ISalesPersonService
    {
        IQueryable<OrderHeaderDTO> GetOrderHeaders(string userId);
        void CreateShipmentXML(int purchaseOrderHeaderID);
    }
}
