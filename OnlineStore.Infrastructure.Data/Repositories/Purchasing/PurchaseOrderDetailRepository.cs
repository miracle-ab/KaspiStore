using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Data.Context;
using OnlineStore.Infrastructure.Data.Main;

namespace OnlineStore.Infrastructure.Data
{
    public class PurchaseOrderDetailRepository : Repository<PurchaseOrderDetail>
    {
        public PurchaseOrderDetailRepository(ItmContext context) : base(context)
        {

        }
    }
}
