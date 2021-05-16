﻿using Ninject.Modules;
using OnlineStore.Infrastructure.Business.Interfaces;
using OnlineStore.Infrastructure.Business.Services;

namespace OnlineStore.Util
{
    public class OrderProcessorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrderProcessorService>().To<OrderProcessorService>();
        }
    }
}