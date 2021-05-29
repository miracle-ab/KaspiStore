using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Enums;
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

        MSMQService MsmqServ { get; set; }

        public SalesPersonService(UnitOfWork uow)
        {
            unitOfWork = uow;
            MsmqServ = new MSMQService();
        }

        public IQueryable<OrderHeaderDTO> GetOrderHeaders(string userId)
        {
            var salesPersonId = unitOfWork.SalesPerson.GetList(i => i.UserId == userId).FirstOrDefault();

            var orderHeaders = from ph in unitOfWork.PurchaseOrderHeader.GetList()
                               where ph.EmployeeID == salesPersonId.BusinessEntityID && ph.Status == 2
                               select new OrderHeaderDTO
                               {
                                   PurchaseOrderID = ph.PurchaseOrderID,
                                   Status = Status.Pending,
                                   OrderDate = ph.OrderDate,
                                   ShipDate = ph.ShipDate,
                                   SubTotal = ph.SubTotal,
                                   TaxAmt = ph.TaxAmt,
                                   Freight = ph.Freight,
                                   TotalDue = ph.TotalDue
                               };

            return orderHeaders;
        }

        public IQueryable<OrderHeaderDTO> GetAllOrderHeaders()
        {
            var currentYear = DateTime.Now.Year;

            var orderHeaders = from ph in unitOfWork.PurchaseOrderHeader.GetList()
                               where ph.OrderDate.Year == currentYear && ph.Status == 2
                               select new OrderHeaderDTO
                               {
                                   PurchaseOrderID = ph.PurchaseOrderID,
                                   Status = Status.Pending,
                                   OrderDate = ph.OrderDate,
                                   ShipDate = ph.ShipDate,
                                   SubTotal = ph.SubTotal,
                                   TaxAmt = ph.TaxAmt,
                                   Freight = ph.Freight,
                                   TotalDue = ph.TotalDue,
                                   SalesPerson = ph.Employee.SalesPerson.AspNetUser.UserName
                               };

            return orderHeaders;
        }

        public SalesProductDTO GetOrderDetails(int purchaseOrderHeaderID)
        {
            var orderHeader = unitOfWork.PurchaseOrderHeader.Get(purchaseOrderHeaderID);

            var aspNetCustomer = unitOfWork.Person.Get((int)orderHeader.PersonID);

            var productsDTO = new List<ProductDTO>();

            foreach (var orderDetail in orderHeader.PurchaseOrderDetails)
            {
                productsDTO.Add(new ProductDTO
                {
                    ProductID = orderDetail.ProductID,
                    Name = orderDetail.Product.Name,
                    Price = orderDetail.UnitPrice,
                    Quantity = orderDetail.OrderQty
                });
            }

            var customerDTO = new CustomerDTO
            {
                FirstName = aspNetCustomer.FirstName,
                LastName = aspNetCustomer.LastName,
                City = aspNetCustomer.BusinessEntity.BusinessEntityAddresses.First().Address.City,
                Address = aspNetCustomer.BusinessEntity.BusinessEntityAddresses.First().Address.AddressLine1,
                Email = aspNetCustomer.EmailAddresses.First().EmailAddress1,
                PhoneNumber = aspNetCustomer.PersonPhones.First().PhoneNumber
            };

            var salesProductDTO = new SalesProductDTO {
                Products = productsDTO,
                Customer = customerDTO
            };


            return salesProductDTO;
        }

        public void CreateShipmentXML(int purchaseOrderHeaderID)
        {
            var orderHeader = unitOfWork.PurchaseOrderHeader.Get(purchaseOrderHeaderID);

            var aspNetCustomer = unitOfWork.Person.Get((int)orderHeader.PersonID);

            XDocument xdoc = new XDocument();
            XElement xclient= new XElement("client");
            XElement xorderHeader = new XElement("orderHeader");

            XAttribute xfirstName = new XAttribute("firstName", aspNetCustomer.FirstName);
            XAttribute xmiddleName = new XAttribute("middleName", aspNetCustomer.MiddleName ?? "");
            XAttribute xlastName = new XAttribute("lastName", aspNetCustomer.LastName);

            XElement xcity = new XElement("city", aspNetCustomer.BusinessEntity.BusinessEntityAddresses.First().Address.City);
            XElement xaddress = new XElement("address", aspNetCustomer.BusinessEntity.BusinessEntityAddresses.First().Address.AddressLine1);
            XElement xemail = new XElement("email", aspNetCustomer.EmailAddresses.First().EmailAddress1);
            XElement xmobilePhone = new XElement("mobilePhone", aspNetCustomer.PersonPhones.First().PhoneNumber);

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

            MsmqServ.SendMessage(xdoc, purchaseOrderHeaderID);

            orderHeader.Status = 3; //Отправлен 
            unitOfWork.PurchaseOrderHeader.Update(orderHeader);
            unitOfWork.Save();
        }
    
        public void ChangeOrderStatus(int purchaseOrderHeaderID)
        {
            var orderHeader = unitOfWork.PurchaseOrderHeader.Get(purchaseOrderHeaderID);
            orderHeader.Status = 4; //Завершен 
            unitOfWork.PurchaseOrderHeader.Update(orderHeader);
            unitOfWork.Save();
        }
    }
}
