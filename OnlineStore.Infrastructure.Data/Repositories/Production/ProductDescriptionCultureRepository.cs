using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Data.Context;
using OnlineStore.Infrastructure.Data.Main;

namespace OnlineStore.Infrastructure.Data
{
    public class ProductDescriptionCultureRepository : Repository<ProductModelProductDescriptionCulture>
    {
        public ProductDescriptionCultureRepository(ItmContext context) : base(context)
        {

        }
    }
}
