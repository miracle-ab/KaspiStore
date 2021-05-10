using OnlineStore.Infrastructure.Business.DTO;
using System.Collections.Generic;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface ICartService
    {
        void AddItem(ProductDTO productDto, int quantity);
        void RemoveLine(ProductDTO productDto);
        int ComputeTotalQuantity();
        decimal ComputeTotalValue();
        void Clear();
    }
}
