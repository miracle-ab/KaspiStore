using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Data.Context;
using OnlineStore.Infrastructure.Data.Main;

namespace OnlineStore.Infrastructure.Data
{
    public class SalesTaxRateRepository : Repository<SalesTaxRate>
    {
        public SalesTaxRateRepository(ItmContext context) : base(context)
        {

        }
    }
}
