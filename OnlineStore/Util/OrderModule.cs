using Ninject.Modules;
using OnlineStore.Infrastructure.Business.Interfaces;
using OnlineStore.Infrastructure.Business.Services;

namespace OnlineStore.Util
{
    public class OrderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductService>().To<ProductService>();
        }
    }
}
