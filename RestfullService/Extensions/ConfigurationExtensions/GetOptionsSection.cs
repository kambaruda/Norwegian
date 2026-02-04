using Core.Models;

namespace RestfullService.Extensions.ConfigurationExtensions
{
    public static class ConfigurationExtensions
    {
        public static T GetOptionsSection<T>(this IConfiguration configuration, string key)
            where T : TokenOptions
        {
            var section = configuration.GetSection(key).Get<T>();
            return section ?? throw new ArgumentNullException(nameof(key), $"{key} is missing in the configuration.");
        }
    }
}
