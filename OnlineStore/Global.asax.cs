using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using OnlineStore.Binders;
using OnlineStore.Infrastructure.Business.Infrastructure;
using OnlineStore.Infrastructure.Business.Services;
using OnlineStore.Util;

namespace OnlineStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // подключение modelbinder
            ModelBinders.Binders.Add(typeof(CartService), new CartModelBinder());

            // внедрение зависимостей
            NinjectModule productModule = new ProductModule();
            NinjectModule orderProcessorModule = new OrderProcessorModule();
            NinjectModule salesPersonModule = new SalesPersonModule();
            NinjectModule serviceModule = new ServiceModule("ItmContext");
            var kernel = new StandardKernel(productModule, orderProcessorModule, salesPersonModule, serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            kernel.Unbind<ModelValidatorProvider>();

        }
    }
}
