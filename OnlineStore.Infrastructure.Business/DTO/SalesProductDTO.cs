using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Business.DTO
{
    public class SalesProductDTO
    {
        public List<ProductDTO> Products { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
