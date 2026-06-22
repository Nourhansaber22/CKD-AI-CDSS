using FluentValidation;

namespace CKD_AI_CDSS.Application.Features.Patient.Commands.UploadLabReport;

public class UploadLabReportCommandValidator
    : AbstractValidator<UploadLabReportCommand>
{
    public UploadLabReportCommandValidator()
    {
        RuleFor(x => x.PatientId)
            .GreaterThan(0);

        RuleFor(x => x.File)
            .NotNull();
    }
}