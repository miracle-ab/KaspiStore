using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Business.DTO;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface IAccountService
    {
        void CreatePerson(PersonDTO personDTO);
    }
}
