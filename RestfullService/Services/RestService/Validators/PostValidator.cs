using Core.Dtos.Requests;
using FluentValidation;

namespace RestfullService.Services.RestService.Validators
{
    public sealed class PostValidator : AbstractValidator<RestfulRequest>
    { 
        public PostValidator()
        {
            RuleFor(x => x.IsAdult).NotNull().NotEmpty().WithMessage("IsAdult is not a valid bool.");
        }
    }
}
