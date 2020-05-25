using System.Net.Http;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace testMvcHttpClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AutofacContainer();
        }

        private static void AutofacContainer()
        {
            var builder = new ContainerBuilder();
            
            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterControllers(assembly);
            
            builder.Register(c =>
            {
                var hostBuilder = new HostBuilder();
                hostBuilder.ConfigureServices(s => s.AddHttpClient());

                return hostBuilder.Build().Services.GetService<IHttpClientFactory>();
            }).SingleInstance();
            
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}