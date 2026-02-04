using Core.Abstracts;
using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace RestfullService.Services.AuthService
{
    [ApiController]
    [Route("api/auth")]
    public sealed class AuthController(
        IHandler<GetJwtRequest, GetJwtResponse> handler) : BaseController
    {
        private readonly IHandler<GetJwtRequest, GetJwtResponse> _handler = handler;

        [HttpGet]
        [SwaggerOperation(
            Summary = "Generate JWT token",
            Description = "Returns a valid JWT token used to authorize protected endpoints",
            Tags = new[] { "Auth" }
        )]
        [ProducesResponseType(typeof(GetJwtResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetJwt()
        {
            var response = _handler.Handle(new GetJwtRequest());
            return CreateOkResponse(response);
        }
    }
}