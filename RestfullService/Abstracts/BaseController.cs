using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Core.Abstracts
{
    public abstract class BaseController : ControllerBase
    {
        public BaseController() { }
        private IActionResult CreateResponse(
            HttpStatusCode statusCode,
            object? body = null,
            IDictionary<string, string>? headers = null,
            string? contentType = null)
        {
            if (headers != null)
                foreach (var (key, value) in headers)
                    Response.Headers[key] = value;

            if (body is null)
                return StatusCode((int)statusCode);

            var result = new ObjectResult(body)
            {
                StatusCode = (int)statusCode
            };

            if (!string.IsNullOrEmpty(contentType))
                result.ContentTypes.Add(contentType);

            return result;
        }

        protected IActionResult CreateOkResponse(
            object? body = null,
            IDictionary<string, string>? headers = null,
            string? contentType = "application/json") =>
            CreateResponse(HttpStatusCode.OK, body, headers, contentType);

        protected IActionResult CreateBadRequestResponse(
            object? body = null,
            IDictionary<string, string>? headers = null,
            string? contentType = "application/json") =>
            CreateResponse(HttpStatusCode.BadRequest, body, headers, contentType);

        protected IActionResult CreateForbiddenResponse(
            object? body = null,
            IDictionary<string, string>? headers = null,
            string? contentType = "application/json") =>
            CreateResponse(HttpStatusCode.Forbidden, body, headers, contentType);
    }
}
