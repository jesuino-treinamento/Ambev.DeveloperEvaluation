using Ambev.DeveloperEvaluation.Integration.Shared;
using FluentAssertions;
using Microsoft.EntityFrameworkCore; 
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Sales
{
    [Collection("Database collection")]
    public class SalesRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;

        public SalesRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void DatabaseShouldContainTestData()
        {
            using var context = _fixture.CreateContext();

            var sales = context.Sales
                .Include(sale => sale.Items) 
                .ToList();

            sales.Should().NotBeEmpty("because test data should have been seeded");

            var firstSale = sales.First();

            firstSale.Should().NotBeNull();
            firstSale.SaleNumber.Should().NotBeNullOrEmpty();
            firstSale.Items.Should().NotBeEmpty("because a sale should contain items");
            firstSale.CustomerId.Should().NotBeEmpty();

            var customer = context.Users.FirstOrDefault(u => u.Id == firstSale.CustomerId);
            customer.Should().NotBeNull("because the sale's customer should exist in the database");
        }
    }
}