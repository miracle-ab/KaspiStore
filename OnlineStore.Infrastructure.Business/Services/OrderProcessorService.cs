using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Interfaces;
using OnlineStore.Infrastructure.Data;

namespace OnlineStore.Infrastructure.Business.Services
{
    public class OrderProcessorService : IOrderProcessorService
    {
        UnitOfWork unitOfWork { get; set; }

        public OrderProcessorService(UnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public void ProcessOrder(CartService cart, ShippingDetailsDTO shippingDetails)
        {
            var employeeID = GetSalesPerson(shippingDetails.Country.ToString());

            var subTotal = cart.ComputeTotalValue();
            var taxAmt = (cart.ComputeTotalValue() * 8) / 100;
            var freight = (cart.ComputeTotalValue() * 25) / 1000;

            var orderHeader = new PurchaseOrderHeader() 
            {
                RevisionNumber = 1,
                Status = 1,
                EmployeeID = employeeID,
                ShipMethodID = 1,
                OrderDate = DateTime.Now,
                SubTotal = subTotal,
                TaxAmt = taxAmt,
                Freight = freight,
                TotalDue = subTotal + taxAmt + freight,
                ModifiedDate = DateTime.Now
            };

            foreach(var item in cart.Lines)
            {
                var orderDetail = new PurchaseOrderDetail()
                {
                    DueDate = DateTime.Now.AddDays(5),
                    OrderQty = (short)item.Quantity,
                    ProductID = item.Product.ProductID,
                    UnitPrice = item.Product.Price,
                    LineTotal = item.Product.Price * item.Quantity,
                    ReceivedQty = item.Quantity,
                    RejectedQty = 0,
                    StockedQty = item.Quantity,
                    ModifiedDate = DateTime.Now
                };

                orderHeader.PurchaseOrderDetails.Add(orderDetail);
            }

            unitOfWork.PurchaseOrderHeader.Create(orderHeader);
            unitOfWork.Save();
        }

        public int GetSalesPerson(string country)
        {
            var salesPerson = (from sp in unitOfWork.SalesPerson.GetList()
                               join st in unitOfWork.SalesTerritory.GetList() on sp.TerritoryID equals st.TerritoryID
                               where st.CountryRegionCode == country
                               orderby sp.SalesYTD
                               select sp.BusinessEntityID
                               ).FirstOrDefault();
            return salesPerson;
        }
    }
}
