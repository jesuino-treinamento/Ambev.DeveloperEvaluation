using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain
{
    public static class CreateUserHandlerTestData
    {
        private static readonly Faker<CreateUserCommand> _validUserFaker = new Faker<CreateUserCommand>()
            .RuleFor(u => u.Username, f => f.Internet.UserName().ClampLength(3, 50))
            .RuleFor(u => u.Password, f => $"P@ssw0rd{f.Random.Number(100, 999)}")
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
            .RuleFor(u => u.Status, f => f.PickRandom<UserStatus>())
            .RuleFor(u => u.Role, f => f.PickRandom<UserRole>())
            .FinishWith((f, u) =>
            {
                if (!u.Email.Contains(u.Username))
                {
                    u.Email = $"{u.Username}@{f.Internet.DomainName()}";
                }
            });

        private static readonly Faker<CreateUserCommand> _invalidUserFaker = new Faker<CreateUserCommand>()
            .RuleFor(u => u.Username, f => f.Random.Bool() ? string.Empty : f.Random.String2(51))
            .RuleFor(u => u.Password, f => f.Random.String2(1, 5))
            .RuleFor(u => u.Email, f => "invalid-email")
            .RuleFor(u => u.Phone, f => "123")
            .RuleFor(u => u.Status, f => (UserStatus)999)
            .RuleFor(u => u.Role, f => (UserRole)999);

        public static CreateUserCommand GenerateValidCommand() => _validUserFaker.Generate();

        public static CreateUserCommand GenerateInvalidCommand() => _invalidUserFaker.Generate();

        public static CreateUserCommand GenerateCommandWithInvalidField(
            string username = null,
            string password = null,
            string email = null,
            string phone = null,
            UserStatus? status = null,
            UserRole? role = null)
        {
            var validCommand = _validUserFaker.Generate();

            if (username != null) validCommand.Username = username;
            if (password != null) validCommand.Password = password;
            if (email != null) validCommand.Email = email;
            if (phone != null) validCommand.Phone = phone;
            if (status.HasValue) validCommand.Status = status.Value;
            if (role.HasValue) validCommand.Role = role.Value;

            return validCommand;
        }

        public static List<CreateUserCommand> GenerateValidCommands(int count) =>
            _validUserFaker.Generate(count);
    }

    public static class FakerExtensions
    {
        public static string ClampLength(this string value, int min, int max)
        {
            if (string.IsNullOrEmpty(value)) return new string('x', min);
            if (value.Length < min) return value.PadRight(min, 'x');
            if (value.Length > max) return value.Substring(0, max);
            return value;
        }
    }
}
