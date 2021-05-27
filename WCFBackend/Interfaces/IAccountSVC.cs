using OnlineStore.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFBackend
{
    [ServiceContract]
    public interface IAccountSVC
    {
        [OperationContract]
        void CreatePerson(PersonDTO personDTO);
    }
}
