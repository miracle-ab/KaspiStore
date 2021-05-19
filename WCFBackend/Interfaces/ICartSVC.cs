using OnlineStore.Infrastructure.Business.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace WCFBackend
{
    [ServiceContract]
    public interface ICartSVC
    {
        
        [OperationContract]
        void AddItem(ProductDTO productDto, int quantity);
        
        [OperationContract]
        void RemoveLine(ProductDTO productDto);
        
        [OperationContract]
        int ComputeTotalQuantity();
        
        [OperationContract]
        decimal ComputeTotalValue();
        
        [OperationContract]
        void Clear();

        [OperationContract]
        IEnumerable<CartLineDTO> Lines();
    }
}
