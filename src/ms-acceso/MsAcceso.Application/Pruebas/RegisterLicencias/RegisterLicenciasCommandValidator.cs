using FluentValidation;

namespace MsAcceso.Application.Pruebas.RegisterLicencias;

public sealed class RegisterLicenciasCommandValidator : AbstractValidator<RegisterLicenciasCommand>
{
    public RegisterLicenciasCommandValidator()
    {
        RuleFor(p => p.Nombre)
        .NotEmpty().WithMessage("El nombre de la licencia no puede ser nulo")
        .MaximumLength(50).WithMessage("El nombre de la licencia no puede tener más de 50 caracteres");
    }
}