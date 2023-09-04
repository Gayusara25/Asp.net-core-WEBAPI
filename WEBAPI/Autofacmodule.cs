

using Autofac;
using WEBAPI.Services;
namespace WEBAPI

{
    public class Autofacmodule : Module
    {
        protected void Load(ContainerBuilder builder)
        {
            //Add transient
            builder.RegisterType<crud>().As<icrud>()
                .InstancePerDependency();
            //builder.RegisterType<service>().As<Iservice>()
            //   .SingleInstance();  Singleton

            //    builder.RegisterType<service>().As<Iservice>()
            //.InstancePerLifetimeScope(); Scoped
        }
    }
}
