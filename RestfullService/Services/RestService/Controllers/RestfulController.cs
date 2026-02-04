using Core.Abstracts;
using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestfullService.RestService.Controllers
{
    [Authorize(Roles = "Admin,User,Manager")]
    [ApiController]
    [Route("api/Restfull")]
    public class RestfulController(
        IHandler<RestfulRequest, RestfulResponse> handler) : BaseController
    {
        private readonly IHandler<RestfulRequest, RestfulResponse> _handler = handler;

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(RestfulResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestfulResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] RestfulRequest request)
        {
            var response = _handler.Handle(request);

            return request.IsAdult
                ? CreateOkResponse(response)
                : CreateBadRequestResponse(response);
        }
    }
}
