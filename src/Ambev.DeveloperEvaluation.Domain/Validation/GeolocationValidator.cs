using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

public class GeolocationValidator : AbstractValidator<Geolocation>
{
    public GeolocationValidator()
    {
        RuleFor(geo => geo.Lat)
            .NotEmpty().WithMessage("Latitude is required")
            .Matches(@"^-?\d{1,3}(\.\d{1,6})?$").WithMessage("Invalid latitude format");

        RuleFor(geo => geo.Long)
            .NotEmpty().WithMessage("Longitude is required")
            .Matches(@"^-?\d{1,3}(\.\d{1,6})?$").WithMessage("Invalid longitude format");
    }
}

