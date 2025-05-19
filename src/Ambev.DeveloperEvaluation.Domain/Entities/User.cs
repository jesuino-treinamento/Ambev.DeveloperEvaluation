using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class User : BaseEntity, IUser
    {

        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        string IUser.Id => Id.ToString();
        string IUser.Username => Username;
        string IUser.Role => Role.ToString();

        public Name Name { get; set; }
        public Address Address { get; set; }
        public Customer Customer { get; set; }
        public User()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public User(string username, string email, string passwordHash, UserRole role, string phone)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            Role = role;
            Phone = phone;
            Status = UserStatus.Active;
            CreatedAt = DateTime.UtcNow;
        }

        public ValidationResultDetail Validate()
        {
            var validator = new UserValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        public void Activate()
        {
            Status = UserStatus.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            Status = UserStatus.Inactive;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Suspend()
        {
            Status = UserStatus.Suspended;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateAddress(string city, string street, int number,
                                string ZipCode, string lat, string @long)
        {
            Address = new Address(
                city,
                street,
                number,
                ZipCode,
                new Geolocation(lat, @long));
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateName(string FirstName, string LastName)
        {
            Name = new Name(
                FirstName ?? throw new ArgumentNullException(nameof(FirstName)),
                LastName ?? throw new ArgumentNullException(nameof(LastName)));
            UpdatedAt = DateTime.UtcNow;
        }
    }
}