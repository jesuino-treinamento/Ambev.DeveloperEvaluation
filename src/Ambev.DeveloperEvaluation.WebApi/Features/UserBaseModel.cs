using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features
{
    public class UserBaseModel
    {
        public Guid Id { get; internal set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public NameModel Name { get; set; }
        public AddressModel Address { get; set; }
        public string Phone { get; set; } = string.Empty;
        public UserStatus Status { get; set; }
        public UserRole Role { get; set; }
    }

    public class NameModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }

    public class AddressModel
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string ZipCode { get; set; }
        public GeolocationModel Geolocation { get; set; }
    }

    public class GeolocationModel
    {
        public string Lat { get; set; }
        public string @Long { get; set; }
    }

}
