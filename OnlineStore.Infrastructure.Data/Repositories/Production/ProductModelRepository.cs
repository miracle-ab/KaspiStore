using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Data.Context;
using OnlineStore.Infrastructure.Data.Main;

namespace OnlineStore.Infrastructure.Data
{
    public class ProductModelRepository : Repository<ProductModel>
    {
        public ProductModelRepository(ItmContext context) : base(context)
        {

        }
    }
}
