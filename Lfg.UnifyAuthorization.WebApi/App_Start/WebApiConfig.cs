using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Configuration;

namespace Lfg.UnifyAuthorization.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            BuildIocContainer(config);
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void BuildIocContainer(HttpConfiguration config)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly()).Where(p => typeof(ApiController).IsAssignableFrom(p) && !p.IsAbstract && p.Name.EndsWith("Controller"));
            Assembly assembly1 = Assembly.Load(ConfigurationManager.AppSettings["Repository"]);
            Assembly assembly2 = Assembly.Load(ConfigurationManager.AppSettings["Application"]);
            containerBuilder.RegisterAssemblyTypes(assembly1).Where(p => p.Name.EndsWith("Repository") && !p.IsAbstract).AsImplementedInterfaces();
            containerBuilder.RegisterAssemblyTypes(assembly2).Where(p => p.Name.EndsWith("App") && !p.IsAbstract).AsImplementedInterfaces();
            IContainer container = containerBuilder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
