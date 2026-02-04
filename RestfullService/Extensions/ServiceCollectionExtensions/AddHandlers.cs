using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Interfaces;
using RestfullService.Services.AuthService.Handlers;
using RestfullService.Services.RestService.Handlers;

namespace RestfullService.Extensions.ServiceCollectionExtensions
{
    public static class AddHandlers
    {
        public static IServiceCollection AddAuthHandlers(this IServiceCollection services) 
            => services.AddScoped<IHandler<GetJwtRequest, GetJwtResponse>, GetJwtHandler>();
        public static IServiceCollection AddRestHandlers(this IServiceCollection services) 
            => services.AddScoped<IHandler<RestfulRequest, RestfulResponse>, PostHandler>();
    }
}
