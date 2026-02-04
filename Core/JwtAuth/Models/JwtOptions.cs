using Core.Models;

namespace Core.JwtAuth.Models
{
    public class JwtOptions : TokenOptions
    {
        public const string SectionName = "JwtOptions";
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required string SecretKey { get; set; }
        public int ExpiryMinutes { get; set; } = 60;

    }

}
