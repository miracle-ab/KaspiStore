using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Interfaces;
using OnlineStore.Infrastructure.Data;
using OnlineStore.Infrastructure.Business.Infrastructure;

namespace OnlineStore.Infrastructure.Business.Services
{
    public class ProductService : IProductService
    {
        UnitOfWork unitOfWork { get; set; }

        public ProductService(UnitOfWork uow)
        {
            unitOfWork = uow;
        }
        
        public IEnumerable<ProductDTO> GetProducts()
        {

            var productInventory = unitOfWork.ProductInventory.GetList().GroupBy(p => p.ProductID).
                  Select(g => new
                  {
                      ProductID = g.Key,
                      Quantity = g.Sum(p => p.Quantity)
                  }); ;


            var products = (from p in unitOfWork.Products.GetList()
                            join prodInvent in productInventory on p.ProductID equals prodInvent.ProductID
                            join prodProdPh in unitOfWork.ProductProductPhoto.GetList() on p.ProductID equals prodProdPh.ProductID
                            join prodPh in unitOfWork.ProductPhoto.GetList() on prodProdPh.ProductPhotoID equals prodPh.ProductPhotoID
                            join prodMod in unitOfWork.ProductModel.GetList() on p.ProductModelID equals prodMod.ProductModelID
                            join prodDescCul in unitOfWork.ProductDescriptionCulture.GetList() on prodMod.ProductModelID equals prodDescCul.ProductModelID
                            join prodDesc in unitOfWork.ProductDescription.GetList() on prodDescCul.ProductDescriptionID equals prodDesc.ProductDescriptionID
                            where prodInvent.Quantity > 0 && prodDescCul.CultureID.Contains("en")
                            select new ProductDTO
                            {
                                ProductID = p.ProductID,
                                Name = p.Name,
                                Color = p.Color,
                                Price = p.ListPrice,
                                Size = p.Size,
                                SizeUnitMeasureCode = p.SizeUnitMeasureCode,
                                Description = prodDesc.Description,
                                Photo = prodPh.LargePhotoFileName,
                                Quantity = prodInvent.Quantity
                            });
        
            return products;
        }

        public ProductDTO GetProduct(int id)
        {
            var product = unitOfWork.Products.Get(id);

            if (product == null)
                throw new ValidationException("Product doesn't find", "");

            string description;

            if (product.ProductModel != null)
            {
                var descCultureEN = product.ProductModel.ProductModelProductDescriptionCultures.First(i => i.CultureID.Contains("en"));
                description = unitOfWork.ProductDescription.Get(descCultureEN.ProductDescriptionID).Description;
            }
            else
            {
                description = "No description";
            }

            ProductProductPhoto photo = product.ProductProductPhotoes.First();
            string photoFileName = photo.ProductPhoto.LargePhotoFileName;


            return new ProductDTO
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Color = product.Color,
                Price = product.ListPrice,
                Size = product.Size,
                SizeUnitMeasureCode = product.SizeUnitMeasureCode,
                Description = description,
                Photo = photoFileName
            };
        }

        public PhotoDTO Image(string filename)
        {
            var img = unitOfWork.ProductPhoto.GetList(i => i.LargePhotoFileName == filename).FirstOrDefault();

            return new PhotoDTO { LargePhoto = img.LargePhoto };
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
