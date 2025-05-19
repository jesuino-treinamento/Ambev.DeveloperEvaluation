using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Guid UserId { get; private set; }

        [JsonIgnore]
        public virtual User User { get; private set; }

        public string DocumentNumber { get; private set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();

        [NotMapped]
        public Name? Name => User?.Name;

        [NotMapped]
        public Address? Address => User?.Address;

        private Customer() { }

        public Customer(Guid userId, string documentNumber)
        {
            UserId = userId;
            DocumentNumber = documentNumber;
        }
    }
}
