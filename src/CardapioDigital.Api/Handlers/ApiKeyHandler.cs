//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;

//namespace CardapioDigital.Api.Handlers
//{
//    /// <summary>
//    /// ApiKey Handler
//    /// </summary>
//    public class ApiKeyHandler : DelegatingHandler
//    {
//        private const string REQUEST_HEADER = "X-CardapioDigital-APIKEY";

//        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//        {
//            //Validate that the api key exists, and if so, validate
//            if (request.Headers.Contains(REQUEST_HEADER) &&
//                ApiKeyService.ApiKeyIsValid(request.Headers.GetValues(REQUEST_HEADER).First()))
//            {
//                //Allow the request to process further down the pipeline
//                return await base.SendAsync(request, cancellationToken);
//            }

//            return await Task.FromResult(request.CreateResponse(HttpStatusCode.Forbidden, "Bad API Key"));
//        }
//    }
//}