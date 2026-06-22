using FluentValidation;

namespace CKD_AI_CDSS.Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator
    : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
              .NotEmpty()
              .MinimumLength(8)
              .Matches("[A-Z]")
              .WithMessage("Password must contain uppercase letter")
              .Matches("[a-z]")
              .WithMessage("Password must contain lowercase letter")
              .Matches("[0-9]")
              .WithMessage("Password must contain number");

        RuleFor(x => x.Role)
            .IsInEnum();

        RuleFor(x => x.Specialization)
            .NotEmpty()
            .When(x => x.Role == Domain.Enums.UserRole.Doctor);

        RuleFor(x => x.ClinicName)
            .NotEmpty()
            .When(x => x.Role == Domain.Enums.UserRole.Doctor);
    }
}