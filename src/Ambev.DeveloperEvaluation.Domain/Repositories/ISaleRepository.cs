
using Ambev.DeveloperEvaluation.Domain.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Sale?> GetByNumberAsync(string saleNumber, CancellationToken cancellationToken);
        Task<IEnumerable<Sale>> GetAllSalesAsync();
        Task<IEnumerable<Sale>> GetByCustomerAsync(Guid customerId, CancellationToken cancellationToken);
        Task<IEnumerable<Sale>> GetByBranchAsync(Guid branchId, CancellationToken cancellationToken);
        Task<IEnumerable<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken);
        Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ProductExistsAsync(Guid productId, CancellationToken cancellationToken);
        Task<(IEnumerable<Sale> sales, int totalCount)> GetAllAsync(int page, int size, string orderBy);
        Task<SaleItem?> GetSaleItemByIdAsync(Guid saleItemId, CancellationToken cancellationToken);
        Task<SaleItem?> GetSaleId_SaleItemByIdAsync(Guid saleId, Guid saleItemId, CancellationToken cancellationToken);
        Task AddSaleItemAsync(SaleItem saleItem, CancellationToken cancellationToken);
        Task<SaleItem> UpdateSaleItemAsync(SaleItem saleitem, CancellationToken cancellationToken);
        Task<Sale> ProcessNewSaleAsync(Sale sale, CancellationToken cancellationToken);
        Task<Sale> CancelSaleAsync(Guid saleId, CancellationToken cancellationToken);
        Task<Sale> CancelSaleItemAsync(Sale sale, Guid itemId, CancellationToken cancellationToken);
        Task<Sale> CalculateSaleTotalsAsync(Sale sale, CancellationToken cancellationToken);
        decimal CalculateTotal(List<SaleItem> items);
        IQueryable<Sale> Query();
        Task<PaginatedList<Sale>> GetAllPaginatedAsync(int page, int size, string orderBy);
    }
}
