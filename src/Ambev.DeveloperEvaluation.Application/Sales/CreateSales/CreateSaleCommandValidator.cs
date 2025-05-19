using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItems;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("ID do cliente é obrigatório")
                .Must(BeAValidGuid).WithMessage("ID do cliente inválido");

            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("ID da filial é obrigatório")
                .Must(BeAValidGuid).WithMessage("ID da filial inválido");

            RuleFor(x => x.Products)
                .NotEmpty().WithMessage("A venda deve conter pelo menos um item")
                .Must(items => items.Count <= 100).WithMessage("A venda não pode conter mais de 100 itens");

            RuleForEach(x => x.Products)
                .SetValidator(new CreateSaleItemCommandValidator());
        }

        private bool BeAValidGuid(Guid guid)
        {
            return guid != Guid.Empty;
        }

    }
}
