using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public class DiscountService : IDiscountService
    {
        public decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Cannot sell more than 20 items per product.");

            var discountRate = GetDiscount(quantity);
            return Math.Round(unitPrice * quantity * discountRate, 2, MidpointRounding.AwayFromZero);
        }

        public decimal GetDiscount(int quantity)
        {
            if (quantity < 4)
                return 0.00m;
            if (quantity >= 10 && quantity <= 20)
                return 0.20m;
            if (quantity >= 4)
                return 0.10m;

            return 0.00m;
        }
    }
}
