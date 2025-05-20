using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Shared
{
    public class DatabaseFixture : IDisposable
    {
        public DefaultContext Context { get; }
        private readonly string _databaseName = $"TestDB_{Guid.NewGuid()}";

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: _databaseName)
                .Options;

            Context = new DefaultContext(options);
            Context.Database.EnsureCreated();
            SeedTestData();
        }

        private void SeedTestData()
        {
            var testUser = new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Email = "test@example.com",
                Status = UserStatus.Active,
                Role = UserRole.Admin
            };

            Context.Users.Add(testUser);

            var testSales = new[]
            {
                new Sale
                {
                    Id = Guid.NewGuid(),
                    SaleNumber = "SALE-001",
                    SaleDate = DateTime.UtcNow.AddDays(-1),
                    CustomerId = testUser.Id,
                    BranchId = Guid.NewGuid(),
                    TotalAmount = 150.75m,
                    Items = new[]
                    {
                        new SaleItem
                        {
                            Id = Guid.NewGuid(),
                            ProductId = Guid.NewGuid(),
                            Quantity = 3,
                            UnitPrice = 50.25m,
                            TotalPrice = 150.75m,
                            IsCancelled = false
                        }
                    },
                    Status = SaleStatus.Completed
                },
                new Sale
                {
                    Id = Guid.NewGuid(),
                    SaleNumber = "SALE-002",
                    SaleDate = DateTime.UtcNow,
                    CustomerId = testUser.Id,
                    BranchId = Guid.NewGuid(),
                    TotalAmount = 200.00m,
                    Items = new[]
                    {
                        new SaleItem
                        {
                            Id = Guid.NewGuid(),
                            ProductId = Guid.NewGuid(),
                            Quantity = 2,
                            UnitPrice = 100.00m,
                            TotalPrice = 200.00m,
                            IsCancelled = false
                        }
                    },
                    Status = SaleStatus.Pending
                }
            };

            Context.Sales.AddRange(testSales);
            Context.SaveChanges();
        }

        public DefaultContext CreateContext()
        {
            return new DefaultContext(
                new DbContextOptionsBuilder<DefaultContext>()
                    .UseInMemoryDatabase(_databaseName)
                    .Options);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}