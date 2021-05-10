using System.Collections.Generic;
using System.Linq;
using OnlineStore.Infrastructure.Business.DTO;
using OnlineStore.Infrastructure.Business.Interfaces;

namespace OnlineStore.Infrastructure.Business.Services
{
    public class CartService : ICartService
    {
        private List<CartLineDTO> lineCollection = new List<CartLineDTO>();

        public void AddItem(ProductDTO productDto, int quantity)
        {
            CartLineDTO line = lineCollection
                .Where(p => p.Product.ProductID == productDto.ProductID)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLineDTO
                {
                    Product = productDto,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(ProductDTO productDto)
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == productDto.ProductID);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(l => l.Product.Price * l.Quantity);

        }

        public int ComputeTotalQuantity()
        {
            return lineCollection.Sum(l => l.Quantity);

        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLineDTO> Lines
        {
            get { return lineCollection; }
        }
    }
}
