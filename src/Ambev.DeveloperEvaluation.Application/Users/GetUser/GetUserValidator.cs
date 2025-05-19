using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser
{
    public class GetUserValidator : AbstractValidator<GetUserCommand>
    {
        public GetUserValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("User ID is required");
        }
    }
}
