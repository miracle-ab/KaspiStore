using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class ProductList
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public decimal Price { get; set; }
    }
}
