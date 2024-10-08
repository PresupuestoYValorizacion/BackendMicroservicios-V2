using FluentValidation;

namespace MsAcceso.Application.Root.Libros.RegisterLibros;

public sealed class RegisterLibroCommandValidator : AbstractValidator<RegisterLibroCommand>
{

    public RegisterLibroCommandValidator()
    {
        RuleFor(o => o.Nombre)
            .NotEmpty().WithMessage("El nombre del libro no puede ser nulo")
            .MaximumLength(100).WithMessage("El nombre del libro no puede tener más de 50 caracteres");
                    
    }
}