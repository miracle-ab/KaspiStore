using Ninject.Modules;
using OnlineStore.ProductServiceReference;

namespace OnlineStore.Util
{
    public class ProductModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductSVC>().To<ProductSVCClient>();
        }
    }
}
