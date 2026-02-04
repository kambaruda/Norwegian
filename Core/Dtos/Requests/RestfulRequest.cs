namespace Core.Dtos.Requests
{
    public class RestfulRequest
    {
        public required string Id { get; set; }
        public required bool IsAdult { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
    }
}
