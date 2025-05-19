using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default);
        Task<Customer?> GetByDocumentAsync(string documentNumber, CancellationToken cancellationToken = default);
        Task<bool> ExistsWithDocumentAsync(string documentNumber, CancellationToken cancellationToken = default);
        Task<Customer?> GetByIdAsync(Guid customerId, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid customerId, CancellationToken cancellationToken);
    }
}