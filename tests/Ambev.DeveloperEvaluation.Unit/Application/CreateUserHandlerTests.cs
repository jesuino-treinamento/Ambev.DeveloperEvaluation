using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application
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
            .RuleFor(u => u.Name, f => new Name
            {
                FirstName = f.Name.FirstName(),
                LastName = f.Name.LastName()
            })
            .RuleFor(u => u.Address, f => new Address
            {
                City = f.Address.City(),
                Street = f.Address.StreetName(),
                Number = f.Random.Number(1, 1000),
                ZipCode = f.Address.ZipCode("#####-###"),
                Geolocation = new Geolocation
                {
                    Lat = f.Address.Latitude().ToString("F6"),
                    Long = f.Address.Longitude().ToString("F6")
                }
            })
            .FinishWith((f, u) =>
            {
                if (!u.Email.Contains(u.Username))
                {
                    u.Email = $"{u.Username}@{f.Internet.DomainName()}";
                }
            });

        public static CreateUserCommand GenerateValidCommand() => _validUserFaker.Generate();

        public static CreateUserCommand GenerateCommandWithInvalidField(
            Action<CreateUserCommand> modifyAction = null)
        {
            var command = _validUserFaker.Generate();
            modifyAction?.Invoke(command);
            return command;
        }
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