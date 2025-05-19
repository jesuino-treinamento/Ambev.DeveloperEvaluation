using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly DefaultContext _context;

    public BranchRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Branch?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Branches
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Branches
            .AsNoTracking()
            .AnyAsync(b => b.Name == name, cancellationToken);
    }
    public async Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        await _context.Branches.AddAsync(branch, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return branch;
    }

    public async Task<Branch?> GetByIdAsync(Guid branchId, CancellationToken cancellationToken)
    {
        return await _context.Branches.FirstOrDefaultAsync(o => o.Id == branchId, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid branchId, CancellationToken cancellationToken)
    {
        return await _context.Branches.AnyAsync(o => o.Id == branchId, cancellationToken);
    }
}
