using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Domain.Core.Entities;

namespace OnlineStore.Models.ViewModels
{
    public class ProductsViewModel
    {
        public IQueryable<Product> Products { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
