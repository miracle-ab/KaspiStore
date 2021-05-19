using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

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
            salesPersonService.CreateShipmentXML(purchaseOrderHeaderID);
        }

        public IQueryable<OrderHeaderDTO> GetOrderHeaders(string userId)
        {
            return salesPersonService.GetOrderHeaders(userId);
        }
    }
}
