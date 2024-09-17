using FluentValidation;

namespace MsAcceso.Application.Opciones.RegisterOpciones;

public sealed class RegisterOpcionCommandValidator : AbstractValidator<RegisterOpcionCommand>
{

    public RegisterOpcionCommandValidator()
    {
        RuleFor(o => o.Nombre)
            .NotEmpty().WithMessage("El nombre de la opcion no puede ser nulo")
            .MaximumLength(100).WithMessage("El nombre de la opcion no puede tener más de 50 caracteres");
                    
    }
}