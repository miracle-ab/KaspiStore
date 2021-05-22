using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Interfaces;
using OnlineStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public void CreateShipmentXML(int purchaseOrderHeaderID)
        {
            var orderHeader = unitOfWork.PurchaseOrderHeader.Get(purchaseOrderHeaderID);

            var aspNetCustomer = unitOfWork.AspNetCustomer.Get(orderHeader.CustomerID ?? 1);

            XDocument xdoc = new XDocument();
            XElement xclient= new XElement("client");
            XElement xorderHeader = new XElement("orderHeader");

            XAttribute xfirstName = new XAttribute("xfirstName", aspNetCustomer.FirstName);
            XAttribute xmiddleName = new XAttribute("xmiddleName", aspNetCustomer.MiddleName ?? "");
            XAttribute xlastName = new XAttribute("xlastName", aspNetCustomer.LastName);

            XElement xcity = new XElement("xcity", aspNetCustomer.City);
            XElement xaddress = new XElement("address", aspNetCustomer.Address);
            XElement xemail = new XElement("email", aspNetCustomer.Email);
            XElement xmobilePhone = new XElement("mobilePhone", aspNetCustomer.PhoneNumber);

            XElement xtotalDue = new XElement("totalDue", orderHeader.TotalDue);

            xclient.Add(xfirstName, xmiddleName, xlastName, xcity, xaddress, xemail, xmobilePhone);
            xorderHeader.Add(xclient);

            foreach (var orderDetail in orderHeader.PurchaseOrderDetails)
            {
                XElement xorderDetail = new XElement("orderDetail");
                XAttribute xname = new XAttribute("name", orderDetail.Product.Name);
                XElement xquantity = new XElement("quantity", orderDetail.OrderQty);
                XElement xprice = new XElement("price", orderDetail.UnitPrice);


                xorderDetail.Add(xname, xquantity, xprice);
                xorderHeader.Add(xorderDetail);


                //Change quantity product after accept by salesPerson
                //var productItem = unitOfWork.ProductInventory.Get(orderDetail.ProductID);
                //productItem.Quantity -= orderDetail.OrderQty;
                //unitOfWork.ProductInventory.Update(productItem);


            }

            xdoc.Add(xorderHeader);
            xorderHeader.Add(xtotalDue);

            xdoc.Save($"C:\\temp\\ShipmentOrder({purchaseOrderHeaderID}).xml");

            orderHeader.Status = 2;
            unitOfWork.PurchaseOrderHeader.Update(orderHeader);
            unitOfWork.Save();
        }
    }
}
