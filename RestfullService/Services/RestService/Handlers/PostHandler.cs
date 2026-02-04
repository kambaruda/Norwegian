using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Interfaces;
using FluentValidation;

namespace RestfullService.Services.RestService.Handlers
{
    public class PostHandler
        : IHandler<RestfulRequest, RestfulResponse>
    {
        private readonly IValidator<RestfulRequest> _validator;

        public PostHandler(IValidator<RestfulRequest> validator)
        {
            _validator = validator;
        }

        public RestfulResponse Handle(RestfulRequest request)
        {
            return request.IsAdult
            ? new RestfulResponse("We gucci")
            : new RestfulResponse("We're not gucci");
        }
    }
}
