using System.Text.Json;

namespace Core.Interfaces
{
    public interface IJsonable<T> where T : class
    {
        public string ToJstring(JsonSerializerOptions? jsonSerializerOptions = default) => JsonSerializer.Serialize(this, GetType() ?? typeof(T), jsonSerializerOptions);
    }
}
