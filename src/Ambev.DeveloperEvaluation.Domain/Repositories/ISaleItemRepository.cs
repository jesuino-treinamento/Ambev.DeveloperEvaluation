using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleItemRepository
    {
        Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default);
        Task<IEnumerable<SaleItem>> GetBySaleAsync(Guid saleId, CancellationToken cancellationToken = default);
        Task<IEnumerable<SaleItem>> GetByProductAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<decimal> GetTotalSoldByProductAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}