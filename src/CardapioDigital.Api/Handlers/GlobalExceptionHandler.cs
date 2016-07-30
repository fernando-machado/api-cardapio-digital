using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace CardapioDigital.Api.Handlers
{
    /// <summary>
    /// Global Exception Handler
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public async override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var exceptionMessage =
                context.Exception.InnerException == null
                    ? context.Exception.Message
                    : context.Exception.InnerException.Message;

#if DEBUG
            exceptionMessage = context.Exception.ToString();
#endif

            context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError, new
            {
                Message = "An unexpected error occurred. Please contact API administrator",
                ExceptionMessage = exceptionMessage,
                OcurredOn = DateTime.Now,
            }));

            await base.HandleAsync(context, cancellationToken);
        }
    }
}