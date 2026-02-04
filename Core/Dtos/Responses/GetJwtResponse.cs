using Core.Interfaces;

namespace Core.Dtos.Responses
{
    public sealed class GetJwtResponse(string token) : IResponse
    {
        public string Token { get; } = token;
    }
}
