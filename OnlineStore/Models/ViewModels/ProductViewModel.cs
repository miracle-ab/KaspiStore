using OnlineStore.Domain.Core.Entities;

namespace OnlineStore.Models.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string ProductNumber { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        public string SizeUnitMeasureCode { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
    }
}
