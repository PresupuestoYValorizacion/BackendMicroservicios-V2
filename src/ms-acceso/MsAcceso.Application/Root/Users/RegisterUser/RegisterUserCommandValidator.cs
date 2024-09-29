using FluentValidation;

namespace MsAcceso.Application.Root.Users.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Username).NotEmpty().WithMessage("El username no puede ser nulo");

        RuleFor(c => c.Email).EmailAddress();
        
        RuleFor(c => c.Password).NotEmpty().MinimumLength(5);

        // RuleFor(c => c.EmpresaId)
        //         .NotEmpty()
        //         .Must(id => Guid.TryParse(id, out _))
        //         .WithMessage("La EmpresaId no puede ser un GUID vacío");


    }
}