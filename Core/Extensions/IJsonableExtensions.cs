using Core.Interfaces;
using System.Text.Json;

namespace Core.Extensions
{
    public static class JsonableExtensions
    {
        public static string ToJstring<T>(this IJsonable<T> jsonable, JsonSerializerOptions? jsonSerializerOptions = default) where T : class
            => JsonSerializer.Serialize(jsonable, typeof(T), jsonSerializerOptions);
    }
}
