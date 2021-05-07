using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Business.DTO
{
    public class ProductDTO
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
