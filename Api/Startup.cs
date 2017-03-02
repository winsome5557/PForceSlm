using System.Web.Http;
using Microsoft.Practices.Unity;
using Owin;
using Serilog;
using ParcelForce.Test.WebApi.MessageHandlers;
using Unity.WebApi;

namespace ParcelForce.Test.WebApi
{
    public class Startup
    {
        public Startup()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.LiterateConsole(outputTemplate: "[{Timestamp:HH:mm:ss} {Level} {CorrelationId}] {Message}{NewLine}{Exception}")
                .CreateLogger();
        }

        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.Routes.MapHttpRoute("API Default", "api/{controller}/{id}",
                        new { id = RouteParameter.Optional });

            httpConfiguration.MessageHandlers.Add(new AddCorrelationIdToLogContextHandler());
            httpConfiguration.MessageHandlers.Add(new AddCorrelationIdToResponseHandler());

            UnityConfig.RegisterComponents();
            httpConfiguration.DependencyResolver = GlobalConfiguration.Configuration.DependencyResolver;
            app.MapSignalR();
            app.UseWebApi(httpConfiguration);
        }
    }
}
