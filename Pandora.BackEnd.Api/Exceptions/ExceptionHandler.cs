using System.Web.Http.ExceptionHandling;
using System.Text;
using System.Web.Http;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System;

namespace Pandora.BackEnd.Api.Exceptions
{
    public class CustomExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new InternalServerErrorResult(
                "An internal error occurred; check the log for more information.",
                Encoding.UTF8, context.Request);
        }
    }

    public class InternalServerErrorResult : IHttpActionResult
    {
        public string Content { get; private set; }
        public Encoding Encoding { get; private set; }
        public HttpRequestMessage Request { get; private set; }

        public InternalServerErrorResult(string pContent, Encoding pEncoding, HttpRequestMessage pRequest)
        {
            Content = pContent ?? throw new ArgumentNullException("content");
            Encoding = pEncoding ?? throw new ArgumentNullException("encoding");
            Request = pRequest ?? throw new ArgumentNullException("request");
        }


        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            response.RequestMessage = Request;
            response.Content = new StringContent(Content, Encoding);
            return response;
        }
    }
}