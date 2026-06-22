using FluentValidation;

namespace CKD_AI_CDSS.Application.Features.Patient.Commands.UpdateProfile;

public class UpdatePatientProfileValidator
    : AbstractValidator<UpdatePatientProfileCommand>
{
    public UpdatePatientProfileValidator()
    {
        RuleFor(x => x.Age)
            .InclusiveBetween(1, 120)
            .When(x => x.Age.HasValue);

        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .When(x => x.Weight.HasValue);

        RuleFor(x => x.Height)
            .GreaterThan(0)
            .When(x => x.Height.HasValue);
    }
}