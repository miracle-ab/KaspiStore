using System.Collections.Generic;
using System.Linq;
using OnlineStore.Infrastructure.Business.DTO;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface IProductService
    {
        ProductDTO GetProduct(int id);
        IEnumerable<ProductDTO> GetProducts();
        IEnumerable<ProductDTO> GetProductsByCategory(int categoryID);
        IEnumerable<ProductDTO> SearchProducts(string titleProduct);
        PhotoDTO Image(string filename);
        void Dispose();
    }
}
