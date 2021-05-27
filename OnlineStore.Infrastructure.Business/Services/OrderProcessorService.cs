﻿using System;
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

        public void ProcessOrder(IEnumerable<CartLineDTO> cart, ShippingDetailsDTO shippingDetails)
        {
            var employeeID = GetSalesPerson(shippingDetails.Country.ToString());

            var customer = CreateCurrentCustomer(shippingDetails);

            var subTotal = CartTotalValue(cart);
            var taxAmt = (CartTotalValue(cart) * 8) / 100;
            var freight = (CartTotalValue(cart) * 25) / 1000;

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

            foreach(var item in cart)
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

            customer.PurchaseOrderHeaders.Add(orderHeader);

            unitOfWork.Person.Create(customer);

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

        public Person CreateCurrentCustomer(ShippingDetailsDTO shippingDetails)
        {
            var busEntity = new BusinessEntity()
            {
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };

            var personCurrent = new Person()
            {
                FirstName = shippingDetails.FirstName,
                MiddleName = shippingDetails.MiddleName,
                LastName = shippingDetails.LastName,
                EmailPromotion = 0,
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };

            var busEntityAddress = new BusinessEntityAddress()
            {
                AddressTypeID = 2,
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };

            var stateProvince = unitOfWork.StateProvince.GetList(i => i.CountryRegionCode == shippingDetails.Country.ToString()).FirstOrDefault();

            var address = new Address()
            {
                AddressLine1 = shippingDetails.Address,
                City = shippingDetails.City,
                StateProvince = stateProvince,
                PostalCode = shippingDetails.PostalCode,
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };

            var personPhone = new PersonPhone()
            {
                PhoneNumber = shippingDetails.MobilePhone,
                PhoneNumberTypeID = 1,
                ModifiedDate = DateTime.Now
            };

            var emailAddress = new EmailAddress()
            {
                EmailAddress1 = shippingDetails.Email,
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };

            busEntity.BusinessEntityAddresses.Add(busEntityAddress);

            address.BusinessEntityAddresses.Add(busEntityAddress);

            personCurrent.BusinessEntity = busEntity;

            personCurrent.EmailAddresses.Add(emailAddress);

            personCurrent.PersonPhones.Add(personPhone);

            unitOfWork.Address.Create(address);

            return personCurrent;
        }
   
        public void ProccesOrderAuthenticated(IEnumerable<CartLineDTO> cart, ShippingDetailsDTO shippingDetails, string userId)
        {
            var person = unitOfWork.Person.GetList(i => i.UserID == userId).FirstOrDefault();

            var employeeID = GetSalesPerson(shippingDetails.Country.ToString());
           
            var personPhone = new PersonPhone()
            {
                PhoneNumber = shippingDetails.MobilePhone,
                PhoneNumberTypeID = 1,
                ModifiedDate = DateTime.Now
            };

            var emailAddress = new EmailAddress()
            {
                EmailAddress1 = shippingDetails.Email,
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };

            person.EmailAddresses.Add(emailAddress);
            person.PersonPhones.Add(personPhone);

            var subTotal = CartTotalValue(cart);
            var taxAmt = (CartTotalValue(cart) * 8) / 100;
            var freight = (CartTotalValue(cart) * 25) / 1000;

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

            foreach (var item in cart)
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

            person.PurchaseOrderHeaders.Add(orderHeader);
            unitOfWork.Save();
        }

        public decimal CartTotalValue(IEnumerable<CartLineDTO> lineCollection)
        {
            return lineCollection.Sum(l => l.Product.Price * l.Quantity);
        }
    }
}
