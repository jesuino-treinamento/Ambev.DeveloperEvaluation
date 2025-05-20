using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Integration.Sales
{
    public class SalesRepositoryIntegrationTests : IDisposable
    {
        private readonly DefaultContext _context;

        public SalesRepositoryIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DefaultContext(options);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
