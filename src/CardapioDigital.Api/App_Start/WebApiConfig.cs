using System.Web.Http.ExceptionHandling;
using CardapioDigital.Api.Handlers;
using CardapioDigital.Infra;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using Elmah.Contrib.WebApi;

namespace CardapioDigital.Api
{
    /// <summary>
    /// WebApiConfig
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register api configurations
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // There must be exactly one exception handler.
            // (There is a default one that may be replaced.)
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // There can be multiple exception loggers.
            // (By default, no exception loggers are registered.)
            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());

            // Delegating Handlers
            //config.MessageHandlers.Add(new ApiKeyHandler());
            config.MessageHandlers.Add(new RequestsLoggingHandler());
            
            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // Web API routes
            config.MapHttpAttributeRoutes();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
            
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            // Enabling dependency resolver for Mvc and WebApi
            var executingAssembly = Assembly.GetExecutingAssembly();
            //Fabrica.Instancia.RegistrarControllersMvc(executingAssembly);
            Fabrica.Instancia.RegistrarControllersApi(executingAssembly);
            config.DependencyResolver = Fabrica.Instancia.WebApiDependencyResolver;
        }
    }
}
