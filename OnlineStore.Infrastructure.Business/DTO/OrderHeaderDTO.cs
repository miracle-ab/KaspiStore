using OnlineStore.Infrastructure.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Business.DTO
{
    public class OrderHeaderDTO
    {
        public int PurchaseOrderID { get; set; }
        public Status Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal Freight { get; set; }
        public decimal TotalDue { get; set; }
        public string SalesPerson { get; set; }
    }
}
