using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSales
{
    public class CancelSaleCommand : IRequest<CancelSaleResult>
    {
        public Guid SaleId { get; set; }

        public ValidationResultDetail Validate()
        {
            var validator = new CancelSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
