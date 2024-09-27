using FluentValidation;

namespace MsAcceso.Application.Root.Opciones.UpdateOpciones;

public sealed class UpdateOpcionCommandValidator : AbstractValidator<UpdateOpcionCommand>
{

    public UpdateOpcionCommandValidator()
    {
        RuleFor(o => o.Nombre)
            .MaximumLength(100).WithMessage("El nombre de la opcion no puede tener más de 50 caracteres");
        
    }
}