using Ninject.Modules;
using OnlineStore.OrderProcessorServiceReference;

namespace OnlineStore.Util
{
    public class OrderProcessorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrderProcessorSVC>().To<OrderProcessorSVCClient>();
        }
    }
}
