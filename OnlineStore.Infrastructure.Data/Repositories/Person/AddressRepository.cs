using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Data.Context;
using OnlineStore.Infrastructure.Data.Main;

namespace OnlineStore.Infrastructure.Data
{
    public class AddressRepository : Repository<Address>
    {
        public AddressRepository(ItmContext context) : base(context)
        {
        }
    }
}
