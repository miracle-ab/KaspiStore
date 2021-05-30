using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class OrderHeaderViewModel
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

    public enum Status
    {
        New = 1,
        Pending = 2,
        Sent = 3,
        Closed = 4
    }
}
