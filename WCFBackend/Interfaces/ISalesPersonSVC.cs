using OnlineStore.Infrastructure.Business.DTO;
using System.Linq;
using System.ServiceModel;

namespace WCFBackend
{
    [ServiceContract]
    public interface ISalesPersonSVC
    {
        [OperationContract]
        IQueryable<OrderHeaderDTO> GetOrderHeaders(string userId);

        [OperationContract]
        IQueryable<OrderHeaderDTO> GetAllOrderHeaders();

        [OperationContract]
        SalesProductDTO GetOrderDetails(int purchaseOrderHeaderID);

        [OperationContract]
        void CreateShipmentXML(int purchaseOrderHeaderID);
    }
}
