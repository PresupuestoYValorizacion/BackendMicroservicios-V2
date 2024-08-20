using FluentValidation;

namespace MsAcceso.Application.Opciones.UpdateOpciones;

public sealed class UpdateOpcionCommandValidator : AbstractValidator<UpdateOpcionCommand>
{

    public UpdateOpcionCommandValidator()
    {
        RuleFor(o => o.Nombre)
            .MaximumLength(50).WithMessage("El nombre de la opcion no puede tener más de 50 caracteres");
        
        RuleFor(o => o.Logo)
        .MaximumLength(50).WithMessage("El logo de la opcion no puede tener más de 50 caracteres");

        RuleFor(o => o.Abreviatura)
            .MaximumLength(10).WithMessage("La abreviatura de la opcion no puede tener más de 10 caracteres");
        
    }
}