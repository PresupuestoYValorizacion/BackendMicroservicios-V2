using FluentValidation;

namespace MsAcceso.Application.MenuOpcions.UpdateMenuOpcion;

public sealed class UpdateMenuOpcionValidator : AbstractValidator<UpdateMenuOpcionCommand>
{
    public UpdateMenuOpcionValidator()
    {
        RuleFor(m => m.OpcionId).NotEmpty().WithMessage("Eliga una opcion valida para poder actualizar.");
        
        RuleFor(m => m.MenuOpcionId).NotEmpty().WithMessage("Eliga una menu opcion valida para poder actualizar.");

    }
}