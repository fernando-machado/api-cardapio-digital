using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using CardapioDigital.Aplicacao.DTO.Core;

namespace CardapioDigital.Api.CustomHandlers
{
    /// <summary>
    /// Global Exception Handler
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public async override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var exceptionMessage = context.Exception.Message;

#if DEBUG
            exceptionMessage = context.Exception.ToString();
#endif

            if (context.Exception is BusinessException)
            {
                context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    ReasonPhrase = "The data provided in the request is invalid",
                    Content = new StringContent(exceptionMessage),
                    OcurredOn = DateTime.Now,
                }));
            }
            else if (context.Exception is NotImplementedException)
            {
                context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.NotImplemented, new
                {
                    ReasonPhrase = "The operation has not been implemented",
                    Content = new StringContent(exceptionMessage),
                    OcurredOn = DateTime.Now,
                }));
            }
            else
            {
                context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError, new
                {
                    ReasonPhrase = "An unexpected error occurred. Please contact API administrator",
                    Content = new StringContent(exceptionMessage),
                    OcurredOn = DateTime.Now,
                }));
            }

            await base.HandleAsync(context, cancellationToken);
        }
    }
}