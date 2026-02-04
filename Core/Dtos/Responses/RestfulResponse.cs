using Core.Interfaces;

namespace Core.Dtos.Responses
{
    public class RestfulResponse(string message) : IResponse
    {
        public string Message { get; set; } = message;
    }
}
