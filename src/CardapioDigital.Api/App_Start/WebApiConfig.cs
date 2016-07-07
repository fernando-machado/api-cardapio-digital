using CardapioDigital.Infra;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;

namespace CardapioDigital.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            #region ESTOQUE

            config.Routes.MapHttpRoute(
                name: "Produtos",
                routeTemplate: "api/produtos/{idProduto}",
                defaults: new { controller = "produtos", idProduto = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //    name: "ProdutosSubcategoria",
            //    routeTemplate: "api/categorias/{idCategoria}/subcategorias/{idSubcategoria}/produtos/{idProduto}",
            //    defaults: new { controller = "produtossubcategoria", idProduto = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "Subcategorias",
            //    routeTemplate: "api/categorias/{idCategoria}/subcategorias/{idSubcategoria}",
            //    defaults: new { controller = "subcategorias", idSubcategoria = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                name: "Categorias",
                routeTemplate: "api/categorias/{idCategoria}",
                defaults: new { controller = "categorias", idCategoria = RouteParameter.Optional }
            );

            #endregion ESTOQUE


            //#region CONTA

            //config.Routes.MapHttpRoute(
            //    name: "ItensContasParciais",
            //    routeTemplate: "api/contas/{idConta}/parciais/{idContaParcial}/itens/{idItem}",
            //    defaults: new { controller = "itens", idItem = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "ContasParciais",
            //    routeTemplate: "api/contas/{idConta}/parciais/{idContaParcial}",
            //    defaults: new { controller = "parciais", idContaParcial = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "Contas",
            //    routeTemplate: "api/contas/{idConta}",
            //    defaults: new { controller = "contas", idConta = RouteParameter.Optional }
            //);

            //#endregion CONTA


            //#region AVALIAÇÕES

            //config.Routes.MapHttpRoute(
            //    name: "AvaliacoesConta",
            //    routeTemplate: "api/contas/{idConta}/avaliacoes/{idAvaliacao}",
            //    defaults: new { controller = "avaliacoes", idAvaliacao = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "Avaliacoes",
            //    routeTemplate: "api/avaliacoes/{idConta}",
            //    defaults: new { controller = "avaliacoes", idConta = RouteParameter.Optional }
            //);

            //#endregion AVALIAÇÕES


            //#region SOLICITAÇÕES

            //config.Routes.MapHttpRoute(
            //    name: "SolicitacoesConta",
            //    routeTemplate: "api/contas/{idConta}/solicitacoes/{idSolicitacao}",
            //    defaults: new { controller = "solicitacoes", idSolicitacao = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "Solicitacoes",
            //    routeTemplate: "api/solicitacoes/{idConta}",
            //    defaults: new { controller = "solicitacoes", idConta = RouteParameter.Optional }
            //);

            //#endregion SOLICITAÇÕES

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
            
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            // Enabling dependency resolver for Mvc4 and WebApi
            var executingAssembly = Assembly.GetExecutingAssembly();
            //Fabrica.Instancia.RegistrarControllersMvc(executingAssembly);
            Fabrica.Instancia.RegistrarControllersApi(executingAssembly);
            config.DependencyResolver = Fabrica.Instancia.WebApiDependencyResolver;
        }
    }
}
