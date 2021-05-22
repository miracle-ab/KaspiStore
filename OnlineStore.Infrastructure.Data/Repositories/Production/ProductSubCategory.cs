using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Data.Context;
using OnlineStore.Infrastructure.Data.Main;

namespace OnlineStore.Infrastructure.Data
{
    public class ProductSubCategoryRepository : Repository<ProductSubcategory>
    {
        public ProductSubCategoryRepository(ItmContext context) : base(context)
        {

        }
    }
}
