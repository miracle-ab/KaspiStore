using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Data.Context;
using OnlineStore.Infrastructure.Data.Main;

namespace OnlineStore.Infrastructure.Data
{
    public class AspNetCustomerRepository : Repository<AspNetCustomer>
    {
        public AspNetCustomerRepository(ItmContext context) : base(context)
        {
        }
    }
}
