using FluentValidation;

namespace MsAcceso.Application.Root.Sistemas.RegisterSistemas;

public sealed class RegisterSistemasCommandValidator : AbstractValidator<RegisterSistemasCommand>
{
    public RegisterSistemasCommandValidator()
    {
        RuleFor(c => c.Nombre).NotEmpty().WithMessage("El nombre del sistema no puede ser nulo.")
        .MaximumLength(100).WithMessage("El nombre del sistema no puede exceder los 100 caracteres.");
        RuleFor(c => c.Logo).NotEmpty().WithMessage("El logo del sistema no puede ser nulo.")
        .MaximumLength(200).WithMessage("El logo del sistema no puede exceder los 100 caracteres.");
        RuleFor(c => c.Url).NotEmpty().WithMessage("La url del sistema no puede ser nulo.")
        .MaximumLength(100).WithMessage("La url del sistema no puede exceder los 100 caracteres.");
    }
}