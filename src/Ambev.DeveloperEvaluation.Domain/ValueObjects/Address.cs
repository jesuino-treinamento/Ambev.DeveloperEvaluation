namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Address
    {
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public int Number { get; set; }
        public string ZipCode { get; set; } = string.Empty;
        public Geolocation Geolocation { get; set; }

        public Address() { }
        public Address(string city, string street, int number, string zipCode, Geolocation geolocation)
        {
            City = city;
            Street = street;
            Number = number;
            ZipCode = zipCode;
            Geolocation = geolocation;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(City))
                throw new ArgumentException("City cannot be empty");

            if (string.IsNullOrWhiteSpace(Street))
                throw new ArgumentException("Street cannot be empty");

            if (Number <= 0)
                throw new ArgumentException("Number must be positive");

            if (string.IsNullOrWhiteSpace(ZipCode))
                throw new ArgumentException("ZipCode cannot be empty");
        }
    }
}