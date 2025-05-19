using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; private set; }
        public string Location { get; private set; }
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        protected Branch() { }
        public Branch(string name, string location)
        {
            Name = name;
            Location = location;
            Sales = new List<Sale>();
        }
    }
}
