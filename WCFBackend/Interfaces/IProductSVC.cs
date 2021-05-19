using OnlineStore.Infrastructure.Business.DTO;
using System.Collections.Generic;
using System.ServiceModel;


namespace WCFBackend
{
    [ServiceContract]
    public interface IProductSVC
    {

        [OperationContract]
        ProductDTO GetProduct(int id);

        [OperationContract]
        IEnumerable<ProductDTO> GetProducts();

        [OperationContract]
        PhotoDTO Image(string filename);

        [OperationContract]
        void Dispose();
    }
}
