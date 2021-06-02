using OnlineStore.Infrastructure.Business.DTO;

namespace OnlineStore.Infrastructure.Business.Interfaces
{
    public interface ICartService
    {
        void AddItem(ProductDTO productDto, int quantity);
        void SubstractQtyItem(ProductDTO productDto, int quantity);
        void RemoveLine(ProductDTO productDto);
        int ComputeTotalQuantity();
        decimal ComputeTotalValue();
        void Clear();
    }
}
