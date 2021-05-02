using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Data.Context;
using OnlineStore.Infrastructure.Data.Main;

namespace OnlineStore.Infrastructure.Data
{
    class ShoppingCartItemRepository : Repository<ShoppingCartItem>
    {
        public ShoppingCartItemRepository(ItmContext context) : base(context)
        {

        }
    }
}
