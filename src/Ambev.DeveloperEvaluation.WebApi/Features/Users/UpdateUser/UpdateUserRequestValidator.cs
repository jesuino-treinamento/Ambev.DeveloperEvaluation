using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Users.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User ID is required")
            .NotEqual(Guid.Empty).WithMessage("User ID cannot be empty");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .Length(3, 50).WithMessage("Username must be between 3 and 50 characters");

            RuleFor(x => x.Password)
                .SetValidator(new PasswordValidator())
                .When(x => !string.IsNullOrEmpty(x.Password));

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone must be in international format");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .SetValidator(new EmailValidator());

            RuleFor(x => x.Status)
                .NotEqual(UserStatus.Unknown).WithMessage("Status cannot be Unknown");

            RuleFor(x => x.Role)
                .NotEqual(UserRole.None).WithMessage("Role cannot be None");

            RuleFor(user => user.Name).NotNull().DependentRules(() =>
            {
                RuleFor(user => user.Name.FirstName).NotEmpty().MaximumLength(50);
                RuleFor(user => user.Name.LastName).NotEmpty().MaximumLength(50);
            });

            RuleFor(user => user.Address).NotNull().DependentRules(() =>
            {
                RuleFor(user => user.Address.City).NotEmpty().MaximumLength(100);
                RuleFor(user => user.Address.Street).NotEmpty().MaximumLength(200);
                RuleFor(user => user.Address.Number).GreaterThan(0);
                RuleFor(user => user.Address.ZipCode).NotEmpty().MaximumLength(20);

                RuleFor(user => user.Address.Geolocation).NotNull().DependentRules(() =>
                {
                    RuleFor(user => user.Address.Geolocation.Lat).NotEmpty().MaximumLength(1);//50
                    RuleFor(user => user.Address.Geolocation.Long).NotEmpty().MaximumLength(1);//50
                });
            });
        }
    }
}