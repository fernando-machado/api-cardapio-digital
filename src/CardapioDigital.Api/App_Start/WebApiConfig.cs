using CardapioDigital.Api.Areas.HelpPage;
using CardapioDigital.Api.CustomHandlers;
using CardapioDigital.Infra;
using Elmah.Contrib.WebApi;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Batch;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

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

            // Install-Package Microsoft.AspNet.OData -Version 5.3.0 
            // Install-Package Microsoft.AspNet.WebApi.OData -Version 5.3.0
            // config.EnableQuerySupport(new QueryableAttribute());

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            config.SetDocumentationProvider(new XmlDocumentationProvider(System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data/CardapioDigitalApi.Docs.xml")));
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpBatchRoute(
                routeName: "WebApiBatch",
                routeTemplate: "api/v1/batch",
                batchHandler: new DefaultHttpBatchHandler(GlobalConfiguration.DefaultServer)
                {
                    ExecutionOrder = BatchExecutionOrder.NonSequential
                });

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            //config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;
            
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            // add support for the 'format' query paramry param
            // cref: http://blogs.msdn.com/b/hongyes/archive/2012/09/02/support-format-in-asp-net-web-api.aspx
            config.Formatters.JsonFormatter.AddQueryStringMapping("$format", "json", "application/json");
            config.Formatters.XmlFormatter.AddQueryStringMapping("$format", "xml", "application/xml");

            
            // Enabling dependency resolver for Mvc and WebApi
            var executingAssembly = Assembly.GetExecutingAssembly();
            //Fabrica.Instancia.RegistrarControllersMvc(executingAssembly);
            Fabrica.Instancia.RegistrarControllersApi(executingAssembly);
            config.DependencyResolver = Fabrica.Instancia.WebApiDependencyResolver;
        }
    }
}
