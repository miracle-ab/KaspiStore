using Ninject.Modules;
using OnlineStore.Infrastructure.Business.Interfaces;
using OnlineStore.Infrastructure.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Util
{
    public class SalesPersonModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISalesPersonService>().To<SalesPersonService>();
        }
    }
}
