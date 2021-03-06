using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Data.Context;
using OnlineStore.Infrastructure.Data.Main;

namespace OnlineStore.Infrastructure.Data
{
    public class ProductDescriptionRepository : Repository<ProductDescription>
    {
        public ProductDescriptionRepository(ItmContext context) : base(context)
        {

        }
    }
}
