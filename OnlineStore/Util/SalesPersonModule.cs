using Ninject.Modules;
using OnlineStore.SalesPersonServiceReference;

namespace OnlineStore.Util
{
    public class SalesPersonModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISalesPersonSVC>().To<SalesPersonSVCClient>();
        }
    }
}
