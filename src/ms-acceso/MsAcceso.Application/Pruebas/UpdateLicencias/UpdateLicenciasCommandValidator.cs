using FluentValidation;
using MsAcceso.Application.Pruebas.UpdateLicencias;

namespace MsAcceso.Application.Pruebas.UpdateLicencias;

public sealed class UpdateLicenciasCommandValidator : AbstractValidator<UpdateLicenciasCommand>
{
    public UpdateLicenciasCommandValidator()
    {
        RuleFor(p => p.Nombre)
        .NotEmpty().WithMessage("El nombre de la licencia no puede ser nulo")
        .MaximumLength(50).WithMessage("El nombre del parametro no puede tener más de 50 caracteres");
    }
}