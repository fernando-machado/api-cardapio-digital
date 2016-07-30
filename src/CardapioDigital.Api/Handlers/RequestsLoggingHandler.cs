using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace CardapioDigital.Api.Handlers
{
    /// <summary>
    /// Logging Handler
    /// </summary>
    public class RequestsLoggingHandler : DelegatingHandler
    {
        private const string SEPARADOR = "--------------------------------------------------------------------------------------------------------------------------------------";
        private static readonly ILog _log = LogManager.GetLogger(typeof(RequestsLoggingHandler).Name);

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //Log request headers and URL
            var requestHeaders = request.Headers.ToDictionary(h => h.Key, h => h.Value);
            var headersToLog = string.Join("\r\n", requestHeaders.Select(h => h.Key + ": " + string.Join(",", h.Value)));
            var body = await request.Content.ReadAsStringAsync();

            _log.DebugFormat("Request to Url: {0}\r\n------------\r\nHeaders: \r\n{1}\r\n------------\r\nBody: \r\n{2}\r\n------------", request.RequestUri, headersToLog, body);

            //Response comes back
            var response = await base.SendAsync(request, cancellationToken);

            //Log response
            var responseContent = response.Content == null ? null : await response.Content.ReadAsStringAsync();
            _log.DebugFormat("Response | StatusCode: {0} | Content: {1}\r\n{2}", (int)response.StatusCode, responseContent, SEPARADOR);

            //Return response
            return response;
        }
    }
}