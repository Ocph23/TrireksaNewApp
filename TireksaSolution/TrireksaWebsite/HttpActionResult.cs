using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace TrireksaWebsite
{
    public class HttpActionResult : IHttpActionResult
    {
        private  HttpStatusCode _statusCode;

        public HttpActionResult(HttpStatusCode statusCode, string message)
        {
            _statusCode = statusCode;
            Message = message;
        }

        public string Message { get; private set; }
        public HttpRequestMessage Request { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(ExecuteResult());
        }

        public HttpResponseMessage ExecuteResult()
        {
            var response = new HttpResponseMessage();

            if (!string.IsNullOrWhiteSpace(Message))
                //response.Content = new StringContent(Message);
                response = Request.CreateErrorResponse(_statusCode, new Exception(Message));

            response.RequestMessage = Request;
            return response;
        }
    }
}