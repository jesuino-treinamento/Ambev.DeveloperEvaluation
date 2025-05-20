
using Ambev.DeveloperEvaluation.Domain.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;
        private readonly ILogger<SaleRepository> _logger;
        public SaleRepository(DefaultContext context, ILogger<SaleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        //public async Task<(IEnumerable<Sale> sales, int totalCount)> GetAllAsync(int page, int size, string orderBy)
        //{
        //    var query = _context.Sales
        //     .Include(s => s.Items).ThenInclude(i => i.Product)
        //     .Include(s => s.Customer).ThenInclude(c => c.User)
        //     .Include(s => s.Branch)
        //     .AsQueryable();

        //    query = orderBy.ToLower() switch
        //    {
        //        "saledate" => query.OrderByDescending(s => s.SaleDate),
        //        "total_amount" => query.OrderByDescending(s => s.TotalAmount),
        //        "status" => query.OrderBy(s => s.Status),
        //        _ => query.OrderByDescending(s => s.SaleDate)
        //    };

        //    var totalCount = await query.CountAsync();

        //    var sales = await query
        //        .Skip((page - 1) * size)
        //        .Take(size)
        //        .ToListAsync();

        //    return (sales, totalCount);
        //}

        public async Task<(IEnumerable<Sale> sales, int totalCount)> GetAllAsync(int page, int size, string orderBy)
        {
            try
            {
                var totalCount = await _context.Sales.CountAsync();

                var query = _context.Sales.AsQueryable();

                query = ApplyOrdering(query, orderBy);

                var saleIds = await query
                    .Skip((page - 1) * size)
                    .Take(size)
                    .Select(s => s.Id)
                    .ToListAsync();

                var sales = await _context.Sales
                    .Where(s => saleIds.Contains(s.Id))
                    .Include(s => s.Items)
                        .ThenInclude(i => i.Product)
                    .Include(s => s.Customer)
                    .Include(s => s.Branch)
                    .AsSplitQuery() 
                    .ToListAsync();

                return (sales, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve sales", ex);
            }
        }

        private IQueryable<Sale> ApplyOrdering(IQueryable<Sale> query, string orderBy)
        {
            return orderBy.ToLower() switch
            {
                "saledate" => query.OrderByDescending(s => s.SaleDate),
                "total_amount" => query.OrderByDescending(s => s.TotalAmount),
                "status" => query.OrderBy(s => s.Status),
                _ => query.OrderByDescending(s => s.SaleDate)
            };
        }
        public async Task<PaginatedList<Sale>> GetAllPaginatedAsync(int page, int size, string orderBy)
        {
            var query = _context.Sales
           .Include(s => s.Items)
               .ThenInclude(i => i.Product)
           .Include(s => s.Customer)
               .ThenInclude(c => c.User)
           .Include(s => s.Branch)
           .AsQueryable();

            query = orderBy.ToLower() switch
            {
                "saledate" => query.OrderByDescending(s => s.SaleDate),
                "total_amount" => query.OrderByDescending(s => s.TotalAmount),
                "status" => query.OrderBy(s => s.Status),
                _ => query.OrderByDescending(s => s.SaleDate)
            };

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginatedList<Sale>(items, totalCount, page, size);
        }
        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Customer).ThenInclude(c => c.User)
                .Include(s => s.Branch)
                .Include(s => s.Items).ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }
        public async Task<Sale?> GetByNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Customer).ThenInclude(c => c.User)
                .Include(s => s.Branch)
                .Include(s => s.Items)
                .ThenInclude(i => i.ProductId)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SaleNumber == saleNumber, cancellationToken);
        }
        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await _context.Sales
                .Include(s => s.Customer).ThenInclude(c => c.User)
                .Include(s => s.Branch)
                .Include(s => s.Items)
                .ThenInclude(i => i.ProductId)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Sale>> GetByCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Customer).ThenInclude(c => c.User)
                .Include(s => s.Items)
                .Where(s => s.CustomerId == customerId)
                .OrderByDescending(s => s.SaleDate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Sale>> GetByBranchAsync(Guid branchId, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Branch)
                .Include(s => s.Items)
                .Where(s => s.BranchId == branchId)
                .OrderByDescending(s => s.SaleDate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .OrderByDescending(s => s.SaleDate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Sales.AddAsync(sale, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cancelar a venda {SaleNumber}.", sale.SaleNumber);
            }
            _logger.LogInformation("Venda criada com sucesso. Número: {SaleNumber}, Cliente: {Customer}", sale.SaleNumber, sale.Customer.Name);
            return sale;
        }

        public async Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken);
            if (sale == null)
                return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> ProductExistsAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.SaleItems.AnyAsync(p => p.ProductId == productId, cancellationToken);
        }
        public async Task<SaleItem?> GetSaleItemByIdAsync(Guid saleItemId, CancellationToken cancellationToken = default)
        {
            return await _context.SaleItems
                .Include(si => si.Product)
                .FirstOrDefaultAsync(si => si.Id == saleItemId, cancellationToken);
        }
        public async Task<SaleItem?> GetSaleId_SaleItemByIdAsync(Guid saleId, Guid saleItemId, CancellationToken cancellationToken)
        {
            return await _context.SaleItems
                .Include(si => si.Sale)
                .Include(s => s.Sale.Customer).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(si => si.Id == saleItemId && si.SaleId == saleId, cancellationToken);
        }
        public async Task AddSaleItemAsync(SaleItem saleItem, CancellationToken cancellationToken = default)
        {
            await _context.SaleItems.AddAsync(saleItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<SaleItem> UpdateSaleItemAsync(SaleItem saleitem, CancellationToken cancellationToken)
        {
            _context.SaleItems.Update(saleitem);
            await _context.SaveChangesAsync(cancellationToken);
            return saleitem;
        }
        public async Task<Sale> ProcessNewSaleAsync(Sale sale, CancellationToken cancellationToken)
        {
            if (sale.Items.Count == 0)
                throw new InvalidOperationException("Sale must have at least one item");

            await CalculateSaleTotalsAsync(sale, cancellationToken);
            return sale;
        }
        public IQueryable<Sale> Query()
        {
            return _context.Sales.AsQueryable();
        }
        public async Task<Sale> CancelSaleAsync(Guid saleId, CancellationToken cancellationToken)
        {
            var sale = await GetByIdAsync(saleId, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {saleId} not found");

            if (sale.IsCancelled)
                throw new InvalidOperationException("Sale is already cancelled");

            sale.CancelSale();

            await CalculateSaleTotalsAsync(sale, cancellationToken);

            _logger.LogInformation("Sale canceled successfully. Number: {SaleNumber}, Customer: {Customer}", sale.SaleNumber, sale.Customer.Name);

            return sale;
        }
        public async Task<Sale> CancelSaleItemAsync(Sale sale, Guid itemId, CancellationToken cancellationToken)
        {
            var item = sale.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new KeyNotFoundException($"Item with ID {itemId} not found in sale");

            if (item.IsCancelled)
                throw new InvalidOperationException("Item is already cancelled");

            item.Cancel();

            await CalculateSaleTotalsAsync(sale, cancellationToken);

            _logger.LogInformation("Sale item canceled successfully. Item ID: {ItemId}, Sale Number: {SaleNumber}", itemId, sale.SaleNumber);


            return sale;
        }
        public Task<Sale> CalculateSaleTotalsAsync(Sale sale, CancellationToken cancellationToken)
        {
            decimal totalAmount = 0;
            decimal totalDiscount = 0;

            foreach (var item in sale.Items.Where(i => !i.IsCancelled))
            {
                totalAmount += item.CalculateTotalAmount();
                totalDiscount += item.Discount;
            }

            sale.UpdateTotalAmount(totalAmount);
            sale.UpdateTotalDiscount(totalDiscount);

            return Task.FromResult(sale);
        }
        public decimal CalculateTotal(List<SaleItem> items)
        {
            return items.Where(i => !i.IsCancelled)
                        .Sum(i => i.CalculateTotalAmount());
        }
    }
}
