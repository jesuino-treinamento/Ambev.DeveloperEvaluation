using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DefaultContext _context;

    public CustomerRepository(DefaultContext context)
    {
        _context = context;
    }
    public async Task<Customer?> GetByDocumentAsync(string documentNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.DocumentNumber == documentNumber, cancellationToken);
    }
    public async Task<bool> ExistsWithDocumentAsync(string documentNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Customers
            .AsNoTracking()
            .AnyAsync(c => c.DocumentNumber == documentNumber, cancellationToken);
    }
    public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await _context.Customers.AddAsync(customer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return customer;
    }
    public async Task<Customer?> GetByIdAsync(Guid customerId, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .Include(c => c.User)
            .FirstOrDefaultAsync(o => o.Id == customerId, cancellationToken);
    }
    public async Task<bool> ExistsAsync(Guid customerId, CancellationToken cancellationToken)
    {
        return await _context.Customers.AnyAsync(c => c.Id == customerId, cancellationToken);
    }
}