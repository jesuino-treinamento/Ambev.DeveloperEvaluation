namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Name
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Name() { }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
                throw new ArgumentException("FirstName cannot be empty");

            if (string.IsNullOrWhiteSpace(LastName))
                throw new ArgumentException("LastName cannot be empty");
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}