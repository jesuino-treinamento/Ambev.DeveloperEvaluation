using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItems;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public DateTime SaleDate { get; set; }
        public List<CreateSaleItemCommand> Products { get; set; } = new();
        public string SaleNumber { get; set; } = string.Empty;
        public bool IsCancelled { get; set; }

        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
