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
    public class AccountSVC : IAccountSVC
    {
        IAccountService accountService;
        public AccountSVC(IAccountService serv)
        {
            accountService = serv;
        }
        public void CreatePerson(PersonDTO personDTO)
        {
            accountService.CreatePerson(personDTO);
        }

        public IQueryable<OrderHeaderDTO> GetClientOrderHeaders(string clientId)
        {
            return accountService.GetClientOrderHeaders(clientId);
        }
    }
}
