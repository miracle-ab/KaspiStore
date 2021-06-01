using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Data.Context;
using OnlineStore.Infrastructure.Data.Main;


namespace OnlineStore.Infrastructure.Data
{
    public class ShipMethodRepository : Repository<ShipMethod>
    {
        public ShipMethodRepository(ItmContext context) : base(context)
        {

        }
    }
}
