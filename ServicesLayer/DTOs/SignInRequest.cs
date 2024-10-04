namespace ServicesLayer.DTOs
{
    public record class SignInRequest
    {
        public required string Login { get; init; }
        public required string Password { get; init; }
    }
}
