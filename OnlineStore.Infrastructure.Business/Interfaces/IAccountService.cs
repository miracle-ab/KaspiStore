using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Business.DTO;
using System.Linq;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface IAccountService
    {
        void CreatePerson(PersonDTO personDTO);
        IQueryable<OrderHeaderDTO> GetClientOrderHeaders(string clientId);
    }
}
