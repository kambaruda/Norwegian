namespace RestfullService.Extensions.ServiceCollectionExtensions
{
    public static class AddServices
    {
        public static IServiceCollection AddNorwegian(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();

            services.AddJwt(config);

            services.AddRestService();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection AddRestService(this IServiceCollection services)
        {
            services.AddRestHandlers();
            services.AddRestValidators();
            return services;
        }
    }
}
