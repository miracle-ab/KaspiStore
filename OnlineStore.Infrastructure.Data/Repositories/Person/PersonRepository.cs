using OnlineStore.Domain.Core.Entities;
using OnlineStore.Infrastructure.Data.Context;
using OnlineStore.Infrastructure.Data.Main;

namespace OnlineStore.Infrastructure.Data
{
    public class PersonRepository : Repository<Person>
    {
        public PersonRepository(ItmContext context) : base(context)
        {

        }
    }
}
