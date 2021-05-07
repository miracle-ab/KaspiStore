using OnlineStore.Domain.Core.Entities;

namespace OnlineStore.Models.ViewModels
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public string SizeUnitMeasureCode { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
    }
}
