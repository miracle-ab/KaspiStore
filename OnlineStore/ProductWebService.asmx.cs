using System.Collections.Generic;
using System.Web.Services;
using AutoMapper;
using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Infrastructure;
using OnlineStore.Infrastructure.Business.Services;
using OnlineStore.Infrastructure.Data;
using OnlineStore.Models.ViewModels;

namespace OnlineStore
{
    /// <summary>
    /// Сводное описание для ProductWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class ProductWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public List<ProductViewModel> ProductList()
        {
            UnitOfWork uof = new UnitOfWork();
            ProductService productService = new ProductService(uof);

            IEnumerable<ProductDTO> productDtos = productService.GetProducts();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            var products = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);

            uof.Dispose();
            return products;
        }

        [WebMethod]
        public ProductViewModel ProductDetail(int id)
        {
            UnitOfWork uof = new UnitOfWork();
            ProductService productService = new ProductService(uof);

            try
            {
                var productDTO = productService.GetProduct(id);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
                var product = mapper.Map<ProductDTO, ProductViewModel>(productDTO);

                uof.Dispose();
                return product;
            }
            catch (ValidationException ex)
            {
                return new ProductViewModel { Name = ex.Message };
            }

        }
    }
}
