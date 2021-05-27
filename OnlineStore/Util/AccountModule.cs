using Ninject.Modules;
using OnlineStore.AccountServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Util
{
    public class AccountModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAccountSVC>().To<AccountSVCClient>();
        }
    }
}
