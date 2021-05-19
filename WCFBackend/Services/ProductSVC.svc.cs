using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Interfaces;
using System.Collections.Generic;

namespace WCFBackend
{
    public class ProductSVC : IProductSVC
    {
        IProductService productService;
        public ProductSVC(IProductService serv)
        {
            productService = serv;
        }

        public void Dispose()
        {
            productService.Dispose();
        }

        public ProductDTO GetProduct(int id)
        {
            return productService.GetProduct(id);
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            return productService.GetProducts();
        }

        public PhotoDTO Image(string filename)
        {
            return productService.Image(filename);
        }
    }
}
