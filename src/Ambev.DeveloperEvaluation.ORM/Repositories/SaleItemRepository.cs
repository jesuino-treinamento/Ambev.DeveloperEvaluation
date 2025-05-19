using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext _context;

    public SaleItemRepository(DefaultContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<SaleItem>> GetBySaleAsync(Guid saleId, CancellationToken cancellationToken = default)
    {
        return await _context.SaleItems
            .AsNoTracking()
            .Where(i => i.SaleId == saleId)
            .Include(i => i.ProductId)
            .ToListAsync(cancellationToken);
    }
    public async Task<IEnumerable<SaleItem>> GetByProductAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await _context.SaleItems
            .AsNoTracking()
            .Where(i => i.ProductId == productId)
            .Include(i => i.Sale)
            .ToListAsync(cancellationToken);
    }
    public async Task<decimal> GetTotalSoldByProductAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await _context.SaleItems
            .AsNoTracking()
            .Where(i => i.ProductId == productId)
            .SumAsync(i => i.Quantity, cancellationToken);
    }
    public async Task<SaleItem> CreateAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
    {
        await _context.SaleItems.AddAsync(saleItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return saleItem;
    }
}