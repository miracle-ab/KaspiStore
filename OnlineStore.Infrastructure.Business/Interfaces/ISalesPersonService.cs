using OnlineStore.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface ISalesPersonService
    {
        IQueryable<OrderHeaderDTO> GetOrderHeaders(string userId);
        void CreateShipmentXML(int purchaseOrderHeaderID);
    }
}
