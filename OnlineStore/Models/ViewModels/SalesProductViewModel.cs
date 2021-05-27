using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class SalesProductViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}
