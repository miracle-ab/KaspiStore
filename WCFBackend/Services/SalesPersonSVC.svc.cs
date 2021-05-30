using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFBackend
{ 
    public class SalesPersonSVC : ISalesPersonSVC
    {
        ISalesPersonService salesPersonService;
        public SalesPersonSVC(ISalesPersonService serv)
        {
            salesPersonService = serv;
        }
        public void CreateShipmentXML(int purchaseOrderHeaderID)
        {
           salesPersonService.CreateShipmentXMLAsync(purchaseOrderHeaderID);
        }

        public SalesProductDTO GetOrderDetails(int purchaseOrderHeaderID)
        {
            return salesPersonService.GetOrderDetails(purchaseOrderHeaderID);
        }

        public IQueryable<OrderHeaderDTO> GetOrderHeaders(string userId)
        {
            return salesPersonService.GetOrderHeaders(userId);
        }

        public IQueryable<OrderHeaderDTO> GetAllOrderHeaders()
        {
            return salesPersonService.GetAllOrderHeaders();
        }
    }
}
