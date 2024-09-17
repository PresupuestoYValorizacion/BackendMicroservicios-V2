using FluentValidation;

namespace MsAcceso.Application.Sistemas.UpdateSistemas;

public sealed class UpdateSistemasCommandValidator : AbstractValidator<UpdateSistemasCommand>
{
    public UpdateSistemasCommandValidator()
    {
        
        RuleFor(c => c.Nombre).MaximumLength(100).WithMessage("El nombre del sistema no puede exceder los 100 caracteres.");
        RuleFor(c => c.Logo).MaximumLength(200).WithMessage("El logo del sistema no puede exceder los 100 caracteres.");
        RuleFor(c => c.Url).MaximumLength(100).WithMessage("La url del sistema no puede exceder los 100 caracteres.");
    }
}