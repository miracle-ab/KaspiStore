using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Data.Context;
using OnlineStore.Infrastructure.Data.Main;


namespace OnlineStore.Infrastructure.Data
{
    public class PurchaseOrderHeaderRepository : Repository<PurchaseOrderHeader>
    {
        public PurchaseOrderHeaderRepository(ItmContext context) : base(context)
        {

        }
    }
}