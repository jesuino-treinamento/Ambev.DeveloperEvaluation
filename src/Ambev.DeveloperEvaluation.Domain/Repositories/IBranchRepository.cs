using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IBranchRepository
    {
        Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default);
        Task<Branch?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken = default);
        Task<Branch?> GetByIdAsync(Guid branchId, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid branchId, CancellationToken cancellationToken);
    }
}