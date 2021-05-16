using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Interfaces;
using OnlineStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Business.Services
{
    public class SalesPersonService : ISalesPersonService
    {
        UnitOfWork unitOfWork { get; set; }
        public SalesPersonService(UnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public IQueryable<OrderHeaderDTO> GetOrderHeaders(string userId)
        {
            var salesPersonId = unitOfWork.SalesPerson.GetList(i => i.UserId == userId).FirstOrDefault();

            var orderHeaders = from ph in unitOfWork.PurchaseOrderHeader.GetList()
                               where ph.EmployeeID == salesPersonId.BusinessEntityID && ph.Status == 1
                               select new OrderHeaderDTO
                               {
                                   PurchaseOrderID = ph.PurchaseOrderID,
                                   Status = ph.Status,
                                   OrderDate = ph.OrderDate,
                                   ShipDate = ph.ShipDate,
                                   SubTotal = ph.SubTotal,
                                   TaxAmt = ph.TaxAmt,
                                   Freight = ph.Freight,
                                   TotalDue = ph.TotalDue
                               };

            return orderHeaders;
        }
    }
}
