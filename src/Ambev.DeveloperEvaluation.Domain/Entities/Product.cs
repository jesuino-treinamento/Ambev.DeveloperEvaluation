using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public Rating Rating { get; set; }
        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
        public Product(string title, decimal price, string category)
        {
            Title = title;
            Price = price;
            Category = category;
            SaleItems = new List<SaleItem>();
        }

        public void UpdateRating(int rating, string count)
        {
            Rating = new Rating(
                rating,
                count
             );
        }

        public void AddSaleItem(SaleItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            SaleItems.Add(item);
        }
    }
}
