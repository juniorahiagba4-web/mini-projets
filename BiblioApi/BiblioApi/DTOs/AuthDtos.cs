namespace BiblioApi.DTOs
{
    public record RegisterRequest(string Nom, string Email, string Password);
    public record LoginRequest(string Email, string Password);
    public record TokenResponse(string Token);
}
