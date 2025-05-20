using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Integration.Sales
{
    public static class SalesTestData
    {
        public static List<Sale> GetTestSales()
        {
            return new List<Sale>
            {
                new Sale
                {
                    Id = Guid.NewGuid(),
                    SaleDate = DateTime.Now.AddDays(-1),
                    TotalAmount = 100.50m,
                    Items = new List<SaleItem>
                    {
                        new SaleItem
                        {
                            ProductId = Guid.NewGuid(),
                            Quantity = 2,
                            UnitPrice = 50.25m
                        }
                    }
                },
            };
        }
    }
}
