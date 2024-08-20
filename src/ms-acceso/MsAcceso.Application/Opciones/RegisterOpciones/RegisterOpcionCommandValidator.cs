using FluentValidation;

namespace MsAcceso.Application.Opciones.RegisterOpciones;

public sealed class RegisterOpcionCommandValidator : AbstractValidator<RegisterOpcionCommand>
{

    public RegisterOpcionCommandValidator()
    {
        RuleFor(o => o.Nombre)
            .NotEmpty().WithMessage("El nombre de la opcion no puede ser nulo")
            .MaximumLength(50).WithMessage("El nombre de la opcion no puede tener más de 50 caracteres");
            

        RuleFor(o => o.Logo).NotEmpty().WithMessage("El logo de la opcion no puede ser nulo")
                            .MaximumLength(50).WithMessage("El logo de la opcion no puede tener más de 50 caracteres");
        

        RuleFor(o => o.Abreviatura).NotEmpty().WithMessage("La abreviatura no puede ser nula")
            .MaximumLength(10).WithMessage("La abreviatura de la opcion no puede tener más de 10 caracteres");
        
    }
}