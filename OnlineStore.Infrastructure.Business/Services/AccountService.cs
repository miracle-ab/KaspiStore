using OnlineStore.Domain.Core.Entities;
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
    }
}
