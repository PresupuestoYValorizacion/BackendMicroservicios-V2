using FluentValidation;

namespace MsAcceso.Application.Root.MenuOpcions.RegisterMenuOpcion;

public sealed class RegisterMenuOpcionValidator : AbstractValidator<RegisterMenuOpcionCommand>
{
    public RegisterMenuOpcionValidator()
    {
        RuleFor(m => m.OpcionId).NotEmpty().WithMessage("No se puede registrar sin elegir una opcion");
        
        RuleFor(m => m.SistemaId).NotEmpty().WithMessage("No se puede registrar sin elegir un menu");
    }
}