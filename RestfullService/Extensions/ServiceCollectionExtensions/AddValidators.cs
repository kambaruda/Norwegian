using FluentValidation;
using RestfullService.Services.RestService.Validators;

namespace RestfullService.Extensions.ServiceCollectionExtensions
{
    public static class AddValidators
    {
        public static IServiceCollection AddRestValidators(this IServiceCollection services)
        {
            return services.AddPostValidator();
        }
        private static IServiceCollection AddPostValidator(this IServiceCollection services) 
            => services.AddValidatorsFromAssemblyContaining<PostValidator>();
    }
}
