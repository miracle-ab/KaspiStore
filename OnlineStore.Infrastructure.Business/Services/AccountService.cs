using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Interfaces;
using OnlineStore.Infrastructure.Data;
using OnlineStore.Infrastructure.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Business.Services
{
    public class AccountService : IAccountService
    {
        UnitOfWork unitOfWork { get; set; }

        public AccountService(UnitOfWork uow)
        {
            unitOfWork = uow;
        }
        public void CreatePerson(PersonDTO personDTO)
        {
            var busEntity = new BusinessEntity()
            {
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };

            var personCurrent = new Person()
            {
                FirstName = personDTO.FirstName,
                MiddleName = personDTO.MiddleName,
                LastName = personDTO.LastName,
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

            var stateProvince = unitOfWork.StateProvince.GetList(i => i.CountryRegionCode == personDTO.Country.ToString()).FirstOrDefault();

            var address = new Address()
            {
                AddressLine1 = personDTO.Address,
                City = personDTO.City,
                StateProvince = stateProvince,
                PostalCode = personDTO.PostalCode,
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now
            };


            busEntity.BusinessEntityAddresses.Add(busEntityAddress);

            address.BusinessEntityAddresses.Add(busEntityAddress);

            personCurrent.BusinessEntity = busEntity;

            personCurrent.UserID = personDTO.UserId;

            unitOfWork.Address.Create(address);

            unitOfWork.Person.Create(personCurrent);

            unitOfWork.Save();
        }

        public IQueryable<OrderHeaderDTO> GetClientOrderHeaders(string clientId)
        {
            var person = unitOfWork.Person.GetList(i => i.UserID == clientId).FirstOrDefault();

            var orderHeaders = from ph in unitOfWork.PurchaseOrderHeader.GetList()
                               where ph.PersonID == person.BusinessEntityID
                               select new OrderHeaderDTO
                               {
                                   PurchaseOrderID = ph.PurchaseOrderID,
                                   Status = (Status)ph.Status,
                                   OrderDate = ph.OrderDate,
                                   ShipDate = ph.ShipDate,
                                   SubTotal = ph.SubTotal,
                                   TaxAmt = ph.TaxAmt,
                                   Freight = ph.Freight,
                                   TotalDue = ph.TotalDue,
                                   Quantity = ph.PurchaseOrderDetails.Where(t => t.PurchaseOrderID == ph.PurchaseOrderID).Sum(i => i.OrderQty)
                               };

            return orderHeaders;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

    }
}
