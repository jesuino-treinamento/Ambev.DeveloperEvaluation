namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser
{
    public sealed class AuthenticateUserResult
    {
        public string Token { get; set; } = string.Empty;

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}