using System.Web;
using System.Web.Http;

namespace CardapioDigital.Api.Controllers
{
    /// <summary>
    /// This is the ErrorController that logs the 404 error to ELMAH and returns a 404 NotFound response to the browser
    /// http://jasonwatmore.com/post/2014/05/03/Getting-ELMAH-to-catch-ALL-unhandled-exceptions-in-Web-API-21.aspx
    /// </summary>
    public class ErrorController : ApiController
    {
        /// <summary>
        /// Logs the 404 error to ELMAH and returns a 404 NotFound response to the browser
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions]
        public IHttpActionResult NotFound(string path)
        {
            // log error to ELMAH
            Elmah.ErrorSignal.FromCurrentContext().Raise(new HttpException(404, "404 Not Found: /" + path));

            // return 404
            return NotFound();
        }
    }
}